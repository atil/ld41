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
    private Renderer _shapeRenderer;

    public void SetData(CardType type, int number)
    {
        Type = type;
        Number = number;

        _numberText.text = GetNumberText(number);
        _shapeRenderer.sharedMaterial = GetSharedMaterial(type);

        if (Type == CardType.Diamond || Type == CardType.Heart)
        {
            _numberText.color = Color.red;
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

    public bool IsColorSameWith(Card c)
    {
        var isThisRed = Type == CardType.Diamond || Type == CardType.Heart;
        var isArgRed = c.Type == CardType.Diamond || c.Type == CardType.Heart;

        return !(isThisRed ^ isArgRed);
    }

    private Material GetSharedMaterial(CardType type)
    {
        Material mat = null;
        switch (type)
        {
            case CardType.None:
                break;
            case CardType.Diamond:
                mat = Resources.Load<Material>("Materials/DiamondMaterial");
                break;
            case CardType.Heart:
                mat = Resources.Load<Material>("Materials/HeartMaterial");
                break;
            case CardType.Spade:
                mat = Resources.Load<Material>("Materials/SpadeMaterial");
                break;
            case CardType.Club:
                mat = Resources.Load<Material>("Materials/ClubMaterial");
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
        return mat;
    }

    private string GetNumberText(int number)
    {
        switch (number)
        {
            case 1: return "A";
            case 11: return "J";
            case 12: return "Q";
            case 13: return "K";
            default: return number.ToString();
        }
    }
}
