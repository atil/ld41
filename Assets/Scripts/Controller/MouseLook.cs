using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MouseLook
{
    private readonly Vector2 _pitchLimits;

    private const float Sensitivity = 2f;
    private readonly Transform _cam;
    private float _pitch;
    private float _yaw;

    private int _isInverted = 1;

    public MouseLook(Transform camTransform)
    {
        _cam = camTransform;
        _pitchLimits = new Vector2(-89f, 89f);
    }

    public Vector3 Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isInverted = _isInverted == 1 ? -1 : 1;
        }

        _pitch -= Sensitivity * Input.GetAxis("Mouse Y") * _isInverted;
        _yaw += Sensitivity * Input.GetAxis("Mouse X");

        _pitch = Mathf.Clamp(_pitch, _pitchLimits.x, _pitchLimits.y);

        // Don't slerp. We want sharp rotation
        _cam.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);

        return _cam.forward;

    }

    // Look at straight ahead to the given world position
    public void LookAt(Vector3 worldPos)
    {
        var q = Quaternion.LookRotation(worldPos - _cam.position);
        _yaw = q.eulerAngles.y;
        _pitch = 0; 
    }
}
