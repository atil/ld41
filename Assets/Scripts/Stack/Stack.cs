using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Stack
{
    protected readonly List<Card> _cards = new List<Card>();
    protected readonly Transform _root;

    public Stack(Transform root)
    {
        _root = root;
    }

    public bool OwnsCard(Card Card)
    {
        return _cards.Contains(Card);
    }

    public virtual void RefreshVisual() { }

    public virtual Card TakeCard()
    {
        return null;
    }
}
