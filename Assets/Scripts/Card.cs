using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _numberText;

    [SerializeField]
    private TextMeshPro _shapeText;

    public void SetWith(CardData cardData)
    {
        _numberText.text = cardData.Number.ToString();
        _shapeText.text = cardData.Type.ToString();
    }
}
