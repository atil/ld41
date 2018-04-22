using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Stack
{
    protected readonly List<CardModel> _cards = new List<CardModel>();
    protected readonly Transform _root;

    public Stack(Transform root)
    {
        _root = root;
    }

    public bool OwnsCard(CardModel cardModel)
    {
        return _cards.Contains(cardModel);
    }

    public virtual void RefreshVisual() { }

    public virtual CardModel TakeCard()
    {
        return null;
    }
}
