using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Table : Stack
{
    public Table(Transform root) : base(root) { }

    public void Add(CardModel cardModel)
    {
        _cards.Add(cardModel);
    }

    public override void RefreshVisual()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            if (i > 0)
            {
                _cards[i].Hide();
            }

            _cards[i].SetPosition(_root.position + (_cards.Count - i) * Vector3.back);
        }
    }

}
