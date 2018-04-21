using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Deck : Stack
{
    private readonly Wastepile _wastepile;

    public Deck(Transform root, Wastepile wastepile) : base(root)
    {
        _wastepile = wastepile;
    }

    public void Add(CardModel cardModel)
    {
        _cards.Add(cardModel);
    }

    public override void RefreshVisual()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            var cardData = _cards[i];
            cardData.Hide();
            cardData.SetPosition(_root.position + Vector3.up * i * 0.025f);
        }
    }
}
