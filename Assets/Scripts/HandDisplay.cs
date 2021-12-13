using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandDisplay : MonoBehaviour
{
    public List<Card> hand;
    public GameObject playerManager;
    public Player player;

    //Hand Spacing
    public float posOffset;
    public float rotOffset;

    public GameObject card;

    private void Awake()
    {
        //player = playerManager.GetComponent<PlayerManager>().player;
    }
    //private void OnEnable()
    //{
    //    GameManager.OnTurnStarted += LoadHand;
    //}
    //private void OnDisable()
    //{
    //    GameManager.OnTurnStarted -= LoadHand;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    LoadHand();
    //}


    public void DisplayHand()
    {
        for (int i = 0; i < hand.Count; i ++)
        {
            float tilt = (float) Random.Range(-1.5f, 1.5f);
            card.GetComponent<CardDisplay>().card = hand[i];
            Vector3 newPos = transform.position;
            newPos.x += (i * posOffset);
            newPos.z = 80;
            var newCard = Instantiate(card, newPos, Quaternion.Euler(0, 0, i * tilt) , transform);
            card.GetComponent<CardDisplay>().card.status = "inHand";
            card.GetComponent<CardDisplay>().card.player = player;

        }
    }

    public void LoadHand()
    {
        player = playerManager.GetComponent<PlayerManager>().player;
        //hand.AddRange(transform.parent.gameObject.GetComponent<PlayerDisplay>().player.hand);
        List<Card> cardList = player.hand;
        for (int i = 0; i <cardList.Count; i++)
        {
            hand.Add(Instantiate(cardList[i]));
        }
        DisplayHand();
        Debug.Log("Hand Loaded");
    }
}
