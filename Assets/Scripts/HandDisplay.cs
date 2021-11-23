using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    public List<Card> hand;

    //Hand Spacing
    public float posOffset;
    public float rotOffset;
    public GameObject gameManager;
    public GameObject card;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().GameStarted += LoadHand;
    }


    public void DisplayHand()
    {
        for (int i = 0; i < hand.Count; i ++)
        {
            float tilt = (float) Random.Range(-1.5f, 1.5f);
            card.GetComponent<CardDisplay>().card = hand[i];
            Vector3 newPos = transform.position;
            newPos.x += (i * posOffset);
            newPos.z = 80;
            Instantiate(card, newPos, Quaternion.Euler(0, 0, i * tilt) , transform);
            card.GetComponent<CardStatus>().status = "inHand";
        }
    }

    public void LoadHand()
    {
        hand.AddRange(transform.parent.gameObject.GetComponent<PlayerDisplay>().player.hand);
        DisplayHand();
        Debug.Log("Hand Loaded");
    }
}
