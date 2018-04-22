using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Wastepile : Stack
{
    public Wastepile(Transform root) : base(root) { }

    public void Put(List<CardModel> cards)
    {
        _cards.AddRange(cards);
        RefreshVisual();
    }

    public override void RefreshVisual()
    {
        var cardCountToReveal = Mathf.Min(3, _cards.Count);
        for (int i = 0; i < _cards.Count; i++)
        {
            if (i < cardCountToReveal)
            {
                var cardView = GameObject.Instantiate(Game.CardPrefab, _root).GetComponent<CardView>();
                cardView.SetWith(_cards[i]);
                _cards[i].IsHidden = false;
                _cards[i].SetPosition(_root.transform.position
                                      + Vector3.right * 0.3f + Vector3.up * 0.01f);
            }
            else
            {
                _cards[i].RemoveVisual();
            }

        }
    }
}
