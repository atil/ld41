using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public Image BlackScreen;
    public AnimationCurve FadeInCurve;

    public IEnumerator Start()
    {
        const float duration = 5f;
        for (var f = 0f; f < duration; f += Time.deltaTime)
        {
            var c = BlackScreen.color;
            c.a = Mathf.Lerp(1f, 0f, FadeInCurve.Evaluate(f / duration));
            BlackScreen.color = c;

            yield return null;
        }
    }
}
