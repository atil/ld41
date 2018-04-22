using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum CardType
{
    None,
    Diamond,
    Heart,
    Spade,
    Club
}

public class CardModel
{
    public CardType Type { get; set; }
    public int Number { get; set; }

    public bool IsHidden
    {
        get { return _isHidden; }
        set
        {
            _isHidden = value;
            if (value)
            {
                Hide();
            }
            else
            {
                Reveal();
            }
        }
    }

    private bool _isHidden;

    public Action Hide;
    public Action Reveal;
    public Action<Vector3> SetPosition;
    public Action RemoveVisual;

    public override string ToString()
    {
        return "[" + Type + " - " + Number + "]";
    }
}
