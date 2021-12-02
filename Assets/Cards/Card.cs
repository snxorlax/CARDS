using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public Player player;
    public int attack;
    public int hp;
    public int manaCost;
    public string cardName;
    public string cardDescription;
    public string status;
    public string type;
    public bool flipped;
}
