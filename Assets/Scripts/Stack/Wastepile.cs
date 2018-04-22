using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Wastepile : Stack
{
    public Wastepile(Transform root) : base(root) { }

    public void Put(List<Card> cards)
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
                _cards[i].SetHidden(false);
                _cards[i].SetPosition(_root.transform.position
                                      + Vector3.right * 0.3f + Vector3.up * 0.01f);
                _cards[i].gameObject.SetActive(true);

            }
            else
            {
                _cards[i].gameObject.SetActive(false);
            }

        }
    }
}
