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
            Cards[i].SetPosition(Root.transform.position + Vector3.up * 0.01f);
        }
    }

    public override bool PutCard(List<Card> cards)
    {
        if (cards.Count > 1)
        {
            return false;
        }

        var card = cards[0];
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

    public override List<Card> TakeCard(Card clickedCard)
    {
        if (Cards.Count == 0)
        {
            return new List<Card>();
        }
        else
        {
            var topCard = Cards[0];
            Cards.Remove(topCard);
            RefreshVisual(false);

            return new List<Card>() {topCard};

        }

    }

    public override bool UndoCardTake(List<Card> cards)
    {
        foreach (var card in cards)
        {
            Cards.Insert(0, card);
        }

        RefreshVisual(false);
        return base.UndoCardTake(cards);
    }
}
