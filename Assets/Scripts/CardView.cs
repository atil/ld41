using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public CardModel Model { get; private set; }

    [SerializeField]
    private TextMeshPro _numberText;

    [SerializeField]
    private TextMeshPro _shapeText;

    public void SetWith(CardModel cardModel)
    {
        _numberText.text = cardModel.Number.ToString();
        _shapeText.text = cardModel.Type.ToString();

        if (cardModel.Type == CardType.Diamond || cardModel.Type == CardType.Heart)
        {
            _numberText.color = Color.red;
            _shapeText.color = Color.red;
        }

        cardModel.Hide += OnHidden;
        cardModel.Reveal += OnReveal;
        cardModel.SetPosition += pos =>
        {
            transform.position = pos;
        };
        cardModel.RemoveVisual += () =>
        {
          
            Destroy(gameObject);
        };

        Model = cardModel;
    }

    public void OnHidden()
    {
        transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    public void OnReveal()
    {
        if (gameObject == null)
        {
            var a = 3;
        }
        transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
