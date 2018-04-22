using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Action<CardView> OnCardViewClicked;

    [SerializeField]
    private CardView _cardView;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var cardView = hit.transform.GetComponent<CardView>();
            if (cardView != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnCardViewClicked(cardView);
                }
            }
        }
    }

    public void SetCard(CardModel cardModel)
    {
        if (cardModel != null)
        {
            _cardView.gameObject.SetActive(true);
            _cardView.SetWith(cardModel);
        }
        else
        {
            _cardView.gameObject.SetActive(false);
        }
    }
}
