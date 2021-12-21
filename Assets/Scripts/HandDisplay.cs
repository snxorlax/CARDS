using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class HandDisplay : NetworkBehaviour
{
    public List<GameObject> handObjects;
    public List<Card> hand;
    public GameObject playerManager;
    public Player player;

    //Hand Spacing
    public float posOffset;
    public float rotOffset;

    public GameObject card;

    private void Start()
    {
        player = playerManager.GetComponent<PlayerManager>().player;
    }
    public void DisplayHand()
    {
        List<Vector3> positions = PositionCardsinHand(handObjects.Count);
        for (int i = 0; i < handObjects.Count; i ++)
        {
            handObjects[i].transform.position = positions[i];
            //var newCard = Instantiate(card, positions[i], Quaternion.Euler(0, 0, i * tilt) , transform);
        }
    }

    public List<Vector3> PositionCardsinHand(int noCards)
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 newPos = transform.position;
        newPos.z = 80;
        for (int i = 0; i < noCards; i++)
        {
            newPos.x += posOffset;
            positions.Add(newPos);
        }
        return positions;
    }
    [Command]
    public void CmdDrawCard(Card newCard)
    {
        Card cardData = Instantiate(newCard);
        hand.Add(cardData);
        //tilt cards
        float tilt = (float) Random.Range(-4f, 4f);
        //hand.Add(newCard);
        card.GetComponent<CardDisplay>().card = cardData;
        var cardObject = Instantiate(card, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + tilt), transform);
        handObjects.Add(cardObject);
        card.GetComponent<CardDisplay>().card.status = "inHand";
        card.GetComponent<CardDisplay>().card.player = player;
        DisplayHand();
    }
}
