using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // send information to handDisplay to display hand which is managed here
    public GameObject handDisplay;
    // all the player's necessary lists and stats
    public Player player;
    public List<Card> deck;
    public List<Card> hand;
    // list of cards in play controlled by player
    public List<Card> play;

    public int handSize;
    //public void OnEnable()
    //{
    //    GameManager.OnTurnStarted += LoadPlayer;
    //}
    private void Start()
    {
        player = Instantiate(player, transform);
        deck = player.deck;
        hand = player.hand;
        play = player.play;
        LoadPlayerHand();
        GameManager.OnDrawStarted += DrawCard;
    }

    private void LoadPlayerHand()
    {
        for (int i = 0; i < handSize; i++)
        {
            DrawCard();
        }
        //handDisplay.GetComponent<HandDisplay>().LoadHand();
    }

    private void DrawCard()
    {
        int cardNo = Random.Range(0, deck.Count - 1);
        hand.Add(deck[cardNo]);
        handDisplay.GetComponent<HandDisplay>().DrawCard(deck[cardNo]);
        deck.RemoveAt(cardNo);
    }
}
