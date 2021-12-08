using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckListDisplay : MonoBehaviour
{
    public Player player;
    public List<Card> deckList;
    public TMP_Text text;

    private void Start()
    {
        deckList = player.deck;
        text.text = "";
        DisplayDeckList();
    }
    public void DisplayDeckList()
    {
        text.text = "";
        foreach (Card c in deckList)
        {
            Debug.Log(c.cardName);
            text.text = string.Concat(text.text, c.cardName + "\n");
        }
    }
}
