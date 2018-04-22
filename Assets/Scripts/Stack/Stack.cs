using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Stack
{
    protected readonly List<Card> Cards = new List<Card>();
    protected readonly Transform Root;

    public Stack(Transform root)
    {
        Root = root;
    }

    public virtual bool OwnsCard(Card card)
    {
        return Cards.Contains(card);
    }

    public virtual void RefreshVisual(bool initial) { }

    public virtual List<Card> TakeCard(Card clickedCard)
    {
        return new List<Card>();
    }

    public virtual bool PutCard(List<Card> cards)
    {
        return false;
    }

    public virtual bool UndoCardTake(List<Card> cards)
    {
        return false;
    }
}
