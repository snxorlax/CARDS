using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text manaCost, attack, hp;
    //for shrouded or vanish
    public Sprite frontCard;
    public Sprite backCard;
    void Start()
    {
        frontCard = GetComponent<SpriteRenderer>().sprite;
        manaCost = transform.GetChild(0).GetComponent<Text>();
        manaCost.text = card.manaCost.ToString();
        attack = transform.GetChild(1).GetComponent<Text>();
        attack.text = card.attack.ToString();
        hp = transform.GetChild(2).GetComponent<Text>();
        hp.text = card.hp.ToString();
    }

    public void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }
}
