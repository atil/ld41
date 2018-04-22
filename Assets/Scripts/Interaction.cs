using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Action<Card> OnCardViewClicked;

    public Transform CardRoot;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var cardView = hit.transform.GetComponent<Card>();
            if (cardView != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnCardViewClicked(cardView);
                }
            }
        }
    }

}
