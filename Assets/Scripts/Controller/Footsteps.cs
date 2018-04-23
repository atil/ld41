using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private const float DistancePerStep = 3f;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _clips;

    [SerializeField]
    private AudioClip _jumpClip;

    [SerializeField]
    private AudioClip _landClip;

    private List<AudioClip> _shuffledClips;
    private Vector3 _prevPos;
    private float _distanceCovered;

    private void Start()
    {
        _shuffledClips = new List<AudioClip>(_clips);
    }

    public void ExternalUpdate(FpsController fpsController)
    {
        if (_distanceCovered > DistancePerStep)
        {
            _distanceCovered = 0;

            _audioSource.PlayOneShot(_shuffledClips[0]);
            _shuffledClips.Shuffle();
        }

        if (fpsController.IsGonnaJump 
            && fpsController.IsGrounded)
        {
            _audioSource.PlayOneShot(_jumpClip);
        }

        if (fpsController.IsLandedThisFrame
            && !fpsController.IsGonnaJump) 
        {
            _audioSource.PlayOneShot(_landClip);
        }

        if (fpsController.IsGrounded)
        {
            _distanceCovered += Vector3.Distance(_prevPos.WithY(0), transform.position.WithY(0));
        }
        _prevPos = transform.position;
    }
}
