﻿using System;
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

    public void Add(Card Card)
    {
        _cards.Add(Card);
    }

    public override void RefreshVisual()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            var cardData = _cards[i];
            _cards[i].SetHidden(true);
            cardData.SetPosition(_root.position + Vector3.up * i * 0.025f);
        }
    }

    public override Card TakeCard()
    {
        if (_cards.Count == 0)
        {

        }
        else
        {
            var cardCountToTake = 3;
            if (_cards.Count == 2)
            {
                cardCountToTake = 2;
            }
            if (_cards.Count == 1)
            {
                cardCountToTake = 1;
            }

            var cardsToWastepile = new List<Card>();
            for (int i = cardCountToTake - 1; i >= 0; i--)
            {
                cardsToWastepile.Add(_cards[0]);
                _cards.RemoveAt(0);
            }
            
            _wastepile.Put(cardsToWastepile);
        }
        return null;

    }
}
