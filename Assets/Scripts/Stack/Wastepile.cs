using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Wastepile : Stack
{
    public Wastepile(Transform root) : base(root) { }

    public List<Card> GiveUpCards()
    {
        foreach (var card in Cards)
        {
            card.gameObject.SetActive(true);
        }
        Cards.Reverse();
        var c = new List<Card>(Cards);
        Cards.Clear();
        return c;
    }

    public void Put(List<Card> cards)
    {
        cards.Reverse();
        Cards.InsertRange(0, cards);
        RefreshVisual(false);
    }

    public override void RefreshVisual(bool initial)
    {
        var cardCountToReveal = Mathf.Min(3, Cards.Count);
        for (int i = 0; i < Cards.Count; i++)
        {
            if (i < cardCountToReveal)
            {
                Cards[i].SetHidden(false);
                Cards[i].SetPosition(Root.transform.position
                                      + Vector3.right * 0.3f * i 
                                      + Vector3.up * -0.02f * i
                                      + Vector3.forward * 0.3f * i);
                Cards[i].gameObject.SetActive(true);

            }
            else
            {
                Cards[i].gameObject.SetActive(false);
            }

        }
    }

    public override List<Card> TakeCard(Card clickedCard)
    {
        if (Cards.Count > 0)
        {
            var topCard = Cards[0];
            Cards.RemoveAt(0);
            return new List<Card> {topCard};
        }
        return new List<Card>();
    }

    public override bool UndoCardTake(List<Card> cards)
    {
        Put(cards);
        return true;
    }
}
