using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public int hp;
    public int mana;
    public List<Card> ownedCards;
    public List<Card> deck;
    public List<Card> hand = new List<Card>();
    //cards in play controlled by player
    public List<Card> play;
}
