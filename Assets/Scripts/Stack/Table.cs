using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Table : Stack
{
    public Table(Transform root) : base(root) { }

    public void Add(Card card)
    {
        _cards.Add(card);
    }

    public override void RefreshVisual()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetHidden(i > 0);
            _cards[i].SetPosition(_root.position + (_cards.Count - i) * Vector3.back);
        }
    }

    public override Card TakeCard()
    {
        if (_cards.Count == 0)
        {
            return null;
        }

        var topCard = _cards[0];
        if (topCard.IsHidden)
        {
            topCard.IsHidden = false;
            RefreshVisual();
            return null;
        }

        _cards.Remove(topCard);
        RefreshVisual();
        return topCard;
    }
}
