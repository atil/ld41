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
        Cards.Insert(0, card);
    }

    public override void RefreshVisual(bool initial)
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            if (initial)
            {
                Cards[i].SetHidden(i > 0);
            }
            else
            {
                Cards[i].SetHidden(Cards[i].IsHidden);
            }
            Cards[i].SetPosition(Root.position + (Cards.Count - i) * Vector3.back);
        }
    }

    public override List<Card> TakeCard(Card clickedCard)
    {
        if (Cards.Count == 0)
        {
            return new List<Card>();
        }

        var topCard = Cards[0];
        if (topCard.IsHidden)
        {
            topCard.SetHidden(false);
            RefreshVisual(false);
            return new List<Card>();
        }

        var takenCards = new List<Card>();
        var index = Cards.IndexOf(clickedCard);
        for (int i = index; i >= 0; i--)
        {
            takenCards.Add(Cards[0]);
            Cards.RemoveAt(0);
        }

        RefreshVisual(false);
        return takenCards;
    }

    public override bool PutCard(List<Card> cards)
    {
        if (Cards.Count == 0)
        {
            if (cards[0].Number == 13)
            {
                foreach (var card in cards)
                {
                    Add(card);
                }

                RefreshVisual(false);
                return true;
            }
        }
        else
        {
            var topCard = Cards[0];
            var topHandCard = cards[0];
            if (!topCard.IsColorSameWith(topHandCard)
                && topCard.Number == topHandCard.Number + 1)
            {
                foreach (var card in cards)
                {
                    Add(card);
                }

                RefreshVisual(false);
                return true;
            }
        }

        return false;
    }

    public override bool UndoCardTake(List<Card> cards)
    {
        cards.Reverse();
        foreach (var card in cards)
        {
            Add(card);
        }
        RefreshVisual(false);
        return true;
    }
}