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
        _cards.Add(card);
    }

    public override void RefreshVisual()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            if (i > 0)
            {
                _cards[i].transform.Rotate(Vector3.up, 180f);
            }

            _cards[i].transform.position = _root.position + (_cards.Count - i) * Vector3.back;
        }
    }

}
