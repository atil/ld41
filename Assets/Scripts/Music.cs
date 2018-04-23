using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource AudioSource;
    public AnimationCurve FadeInCurve;

    public IEnumerator Start()
    {
        var vol = AudioSource.volume;
        const float duration = 5f;
        for (var f = 0f; f < duration; f += Time.deltaTime)
        {
            AudioSource.volume = Mathf.Lerp(0f, vol, FadeInCurve.Evaluate(f / duration));
            yield return null;
        }
    }
}
