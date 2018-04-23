using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Action<Card> OnCardViewClicked;
    public Action OnCardUndo;

    public Transform CardRoot;

    public AudioSource AudioSource;
    public AudioClip TakeClip;
    public AudioClip DropClip;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
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

        if (Input.GetMouseButtonDown(1))
        {
            OnCardUndo();
        }

    }

    public void PlayTakeSound()
    {
        AudioSource.PlayOneShot(TakeClip);
    }

    public void PlayDropSound()
    {
        AudioSource.PlayOneShot(DropClip);
    }
}
