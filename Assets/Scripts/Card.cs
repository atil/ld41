using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public enum CardType
{
    None,
    Diamond,
    Heart,
    Spade,
    Club
}

public class Card : MonoBehaviour
{
    public CardType Type;
    public int Number;
    public bool IsHidden;

    [SerializeField]
    private TextMeshPro _numberText;

    [SerializeField]
    private TextMeshPro _shapeText;


    public void SetData(CardType type, int number)
    {
        Type = type;
        Number = number;

        if (Type == CardType.Diamond || Type == CardType.Heart)
        {
            _numberText.color = Color.red;
            _shapeText.color = Color.red;
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetHidden(bool val)
    {
        IsHidden = val;
        if (val)
        {
            transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
