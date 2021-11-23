using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public Player player;
    public Text playerHp, playerMana, deckCount;
    public GameObject gameManager;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().GameStarted += LoadDeck;
    }
    void Update()
    {
        playerHp = transform.GetChild(0).GetComponent<Text>();
        playerHp.text = "Health: " +player.hp.ToString();
        playerMana = transform.GetChild(1).GetComponent<Text>();
        playerMana.text = "Mana: " +player.mana.ToString();
        deckCount = transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        deckCount.text = player.deck.Count.ToString();
    }

    public static void LoadDeck()
    {
        Debug.Log("Game Started, Deck Loaded");
    }
}
