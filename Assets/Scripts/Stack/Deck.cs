using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Deck : Stack
{
    private readonly Wastepile _wastepile;
    private readonly Card _bottomCard;

    public Deck(Transform root, Wastepile wastepile) : base(root)
    {
        _bottomCard = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DeckBottomCard"), Root).GetComponent<Card>();
        _wastepile = wastepile;
    }

    public void Add(Card Card)
    {
        Cards.Add(Card);
    }

    public override bool OwnsCard(Card card)
    {
        if (card == _bottomCard)
        {
            return true;
        }

        return base.OwnsCard(card);
    }

    public override void RefreshVisual(bool initial)
    {
        for (var i = 0; i < Cards.Count; i++)
        {
            var cardData = Cards[i];
            Cards[i].SetHidden(true);
            cardData.SetPosition(Root.position + Vector3.up * (Cards.Count - i) * 0.025f);
        }
    }

    public override List<Card> TakeCard(Card clickedCard)
    {
        if (Cards.Count == 0)
        {
            Cards.AddRange(_wastepile.GiveUpCards());
            RefreshVisual(false);
        }
        else
        {
            var cardCountToTake = 3;
            if (Cards.Count == 2)
            {
                cardCountToTake = 2;
            }
            if (Cards.Count == 1)
            {
                cardCountToTake = 1;
            }

            var cardsToWastepile = new List<Card>();
            for (int i = cardCountToTake - 1; i >= 0; i--)
            {
                cardsToWastepile.Add(Cards[0]);
                Cards.RemoveAt(0);
            }
            
            _wastepile.Put(cardsToWastepile);
        }
        return new List<Card>();

    }
}
