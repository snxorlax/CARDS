using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    public Player player;
    public List<Card> deck;
    public List<Card> hand;
    private void Start()
    {
        player = GetComponent<PlayerManager>().player;
        player.deck = deck;
        player.hand = hand;
    }
    public void PlayerDraw()
    {
        int cardNo = Random.Range(0, deck.Count - 1);
        hand.Add(deck[cardNo]);
        deck.RemoveAt(cardNo);

    }
}
