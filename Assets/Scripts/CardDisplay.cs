using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text manaCost, attack, hp;
    void Start()
    {
        manaCost = transform.GetChild(0).GetComponent<Text>();
        manaCost.text = card.manaCost.ToString();
        attack = transform.GetChild(1).GetComponent<Text>();
        attack.text = card.attack.ToString();
        hp = transform.GetChild(2).GetComponent<Text>();
        hp.text = card.hp.ToString();
    }
}
