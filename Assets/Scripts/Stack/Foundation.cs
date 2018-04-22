using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Foundation : Stack
{
    private readonly Card _bottomCard;

    public Foundation(Transform root) : base(root)
    {
        _bottomCard = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DeckBottomCard"), Root)
            .GetComponent<Card>();
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
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].gameObject.SetActive(i == 0);
            Cards[i].SetHidden(i != 0);
            Cards[i].SetPosition(Root.transform.position);
        }
    }

    public override bool PutCard(Card card)
    {
        if (Cards.Count == 0)
        {
            if (card.Number == 1)
            {
                Cards.Insert(0, card);
                RefreshVisual(false);
                return true;
            }
        }
        else
        {
            var topCard = Cards[0];
            if (topCard.IsColorSameWith(card)
                && card.Number == topCard.Number + 1)
            {

                Cards.Insert(0, card);
                RefreshVisual(false);
                return true;
            }
        }

        return false;
    }

    public override Card TakeCard()
    {
        if (Cards.Count == 0)
        {
            return null;
        }
        else
        {
            var topCard = Cards[0];
            Cards.Remove(topCard);
            RefreshVisual(false);

            return topCard;
        }

    }

    public override bool UndoCardTake(Card card)
    {
        Cards.Insert(0, card);
        RefreshVisual(false);
        return base.UndoCardTake(card);
    }
}
