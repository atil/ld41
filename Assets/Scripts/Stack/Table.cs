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

    public override Card TakeCard()
    {
        if (Cards.Count == 0)
        {
            return null;
        }

        var topCard = Cards[0];
        if (topCard.IsHidden)
        {
            topCard.SetHidden(false);
            RefreshVisual(false);
            return null;
        }

        Cards.Remove(topCard);
        RefreshVisual(false);
        return topCard;
    }

    public override bool PutCard(Card card)
    {
        if (Cards.Count == 0)
        {
            if (card.Number == 13)
            {
                Add(card);
                RefreshVisual(false);
                return true;
            }
        }
        else
        {
            var topCard = Cards[0];
            if (!topCard.IsColorSameWith(card)
                && topCard.Number == card.Number + 1)
            {
                Add(card);
                RefreshVisual(false);
                return true;
            }
        }

        return false;
    }

    public override bool UndoCardTake(Card card)
    {
        Add(card);
        RefreshVisual(true);
        return true;
    }
}
