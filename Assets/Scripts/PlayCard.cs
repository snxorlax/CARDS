using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayCard : MonoBehaviour
{
    //color fields to indicate before playing cards
    public Color original;
    public Color hoverColor;

    //the renderer of the player panel
    public SpriteRenderer renderer;

    //list of cards that represent the cards in play
    public List<Card> inPlay;
    public List<GameObject> unitObjects;
    public List<GameObject> vaObjects;

    //center of transform for played cards
    public float centerY1;
    public float centerY2;

    //new scale in play, smaller than in hand
    public float newScale;

    public GameObject placementIndicator;
    public GameObject cardTemplate;


    private void Start()
    {
        unitObjects = new List<GameObject>();
        vaObjects = new List<GameObject>();
        renderer = GetComponent<SpriteRenderer>();
        original = renderer.color;
        hoverColor = original;
        hoverColor.r += .15f;
        hoverColor.g += .15f;
        hoverColor.b += .15f;

    }

    private void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //visual feedback for card about to be played
        if (collision.CompareTag("PlayerCard"))
        {
            renderer.color = hoverColor;
            Debug.Log("Collided");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //reset color once card is played
        if (renderer.color != original)
        {
            renderer.color = original;
            placementIndicator.SetActive(false);
        }    
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        //distance between cards in play
        float unitStep = .04f;
        //final scale of card in play
        Vector3 scale;
        if (collision.gameObject.GetComponent<CardDisplay>().card.type == "art")
        {

            if (collision.GetComponent<CardStatus>().status == "inHand" && Input.GetMouseButton(0) == true)
            {
                SetIndicator(collision.gameObject, vaObjects, unitStep);
            }
            if (collision.CompareTag("PlayerCard") && Input.GetMouseButton(0) == false)
            {
                placementIndicator.SetActive(false);

                //change status of card and add to inPlay
                if (collision.GetComponent<CardStatus>().status != "inPlay")
                {
                    if (vaObjects.Count == 0)
                    {
                        vaObjects.Add(collision.gameObject);
                    }
                    //add to cardObject list after checking relative location
                    else
                    {

                        for (int i = 0; i < vaObjects.Count; i++)
                        {
                            //when lower than 0, just insert at 0
                            if (i == 0 && collision.gameObject.transform.position.x < vaObjects[i].transform.position.x)
                            {
                                    vaObjects.Insert(0, collision.gameObject);
                            }
                            //when higher than top of list, add gameobject
                        if (i == vaObjects.Count - 1 && collision.gameObject.transform.position.x > vaObjects[i].transform.position.x)
                        {
                                    vaObjects.Add(collision.gameObject);
                            }
                            if (i != vaObjects.Count -1)
                            {
                                if (collision.gameObject.transform.position.x > vaObjects[i].transform.position.x && collision.gameObject.transform.position.x < vaObjects[i+1].transform.position.x)
                                {

                                    vaObjects.Insert(i+1, collision.gameObject);
                                }
                            }
                        }
                    }
                    //cardObjects.Add(collision.gameObject);
                    //once played, add card to inPlay list
                    collision.GetComponent<CardStatus>().status = "inPlay";
                    inPlay.Add(collision.GetComponent<CardDisplay>().card);
                    collision.transform.parent.GetComponent<HandDisplay>().hand.Remove(collision.GetComponent<CardDisplay>().card);
                }
                //return to original color
                renderer.color = original;
                // reset rotation once card is played
                collision.transform.rotation = Quaternion.Euler(0, 0, 0);

                //set x positions
                PositionCards(collision.gameObject, unitStep, vaObjects.Count);
                //set x positions
                DisplayCards(PositionCards(cardTemplate, unitStep, vaObjects.Count), vaObjects);

                //set y position for cards in play
                collision.transform.position = new Vector3(collision.transform.position.x, centerY2, collision.transform.position.z);

                //scales down in play cards according to public variable newScale
                scale = collision.transform.localScale;
                scale *= .7f;
                scale.x = Mathf.Clamp(scale.x, newScale, scale.x);
                scale.y = Mathf.Clamp(scale.y, newScale, scale.y);
                scale.z = Mathf.Clamp(scale.z, newScale, scale.z);

                collision.transform.localScale = scale;

                //Logs variable representing the width of a card
                Debug.Log(collision.GetComponent<SpriteRenderer>().bounds.size.x);
            }
        }
        if (collision.gameObject.GetComponent<CardDisplay>().card.type == "unit")
        {

            if (collision.GetComponent<CardStatus>().status == "inHand" && Input.GetMouseButton(0) == true)
            {
                SetIndicator(collision.gameObject, unitObjects, unitStep);
            }
            if (collision.CompareTag("PlayerCard") && Input.GetMouseButton(0) == false)
            {
                placementIndicator.SetActive(false);

                //change status of card and add to inPlay
                if (collision.GetComponent<CardStatus>().status != "inPlay")
                {
                    if (unitObjects.Count == 0)
                    {
                        unitObjects.Add(collision.gameObject);
                    }
                    //add to cardObject list after checking relative location
                    else
                    {

                        for (int i = 0; i < unitObjects.Count; i++)
                        {
                            //when lower than 0, just insert at 0
                            if (i == 0 && collision.gameObject.transform.position.x < unitObjects[i].transform.position.x)
                            {
                                    unitObjects.Insert(0, collision.gameObject);
                            }
                            //when higher than top of list, add gameobject
                            if (i == unitObjects.Count - 1 && collision.gameObject.transform.position.x > unitObjects[i].transform.position.x)
                            {
                                    unitObjects.Add(collision.gameObject);
                            }
                            if (i != unitObjects.Count -1)
                            {
                                if (collision.gameObject.transform.position.x > unitObjects[i].transform.position.x && collision.gameObject.transform.position.x < unitObjects[i+1].transform.position.x)
                                {

                                    unitObjects.Insert(i+1, collision.gameObject);
                                }
                            }
                        }
                    }
                    //cardObjects.Add(collision.gameObject);
                    //once played, add card to inPlay list
                    collision.GetComponent<CardStatus>().status = "inPlay";
                    inPlay.Add(collision.GetComponent<CardDisplay>().card);
                    collision.transform.parent.GetComponent<HandDisplay>().hand.Remove(collision.GetComponent<CardDisplay>().card);
                }
                //return to original color
                renderer.color = original;
                // reset rotation once card is played
                collision.transform.rotation = Quaternion.Euler(0, 0, 0);

                //set x positions
                PositionCards(collision.gameObject, unitStep, unitObjects.Count);
                //set x positions
                DisplayCards(PositionCards(cardTemplate, unitStep, unitObjects.Count), unitObjects);

                //set y position for cards in play
                    collision.transform.position = new Vector3(collision.transform.position.x, centerY1, collision.transform.position.z);

                //scales down in play cards according to public variable newScale
                scale = collision.transform.localScale;
                scale *= .7f;
                scale.x = Mathf.Clamp(scale.x, newScale, scale.x);
                scale.y = Mathf.Clamp(scale.y, newScale, scale.y);
                scale.z = Mathf.Clamp(scale.z, newScale, scale.z);

                collision.transform.localScale = scale;

                //Logs variable representing the width of a card
                Debug.Log(collision.GetComponent<SpriteRenderer>().bounds.size.x);
            }
        }

    }
    //method centers cards symmetrically
    public List<float> PositionCards(GameObject card, float step, int numCards)
    {
        float center = 1.06f;
         List<float> positions = new List<float>();
        float cardWidth = card.GetComponent<SpriteRenderer>().bounds.size.x;
        float totalWidth = (numCards * cardWidth) + (step * (numCards - 1));
        float startX = -1 * (totalWidth / 2) + center;

        for (int i = 0; i < numCards; i++)
        {
            positions.Add(startX + step* i + cardWidth * i);
        }
        return positions;

    }

    public void DisplayCards(List<float> positions, List<GameObject> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = new Vector3(positions[i], cards[i].transform.position.y, cards[i].transform.position.z);
        }
    }

    public void SetIndicator(GameObject card, List<GameObject> cardList, float step)
    {

        for (int i = 0; i < cardList.Count; i++)
        {

            float width = cardTemplate.GetComponent<SpriteRenderer>().bounds.size.x;
            float offsetX = (width + step) / 2;

                if (i == 0 && card.transform.position.x < cardList[i].transform.position.x)
                    {
                placementIndicator.transform.position = new Vector3(cardList[i].transform.position.x - offsetX, cardList[i].transform.position.y, cardList[i].transform.position.z);
                    }
                if (i == cardList.Count - 1 && card.transform.position.x > cardList[i].transform.position.x)
                {
                    placementIndicator.transform.position = new Vector3(cardList[i].transform.position.x + offsetX, cardList[i].transform.position.y, cardList[i].transform.position.z);
                }
                if (i != cardList.Count -1)
                {
                    if (card.transform.position.x > cardList[i].transform.position.x && card.transform.position.x < cardList[i+1].transform.position.x)
                    {
                        placementIndicator.transform.position = new Vector3(cardList[i].transform.position.x + offsetX, cardList[i].transform.position.y, cardList[i].transform.position.z);
                    }
                }
        }
        if (cardList.Count > 0)
        {
            placementIndicator.SetActive(true);
        }
    }
    
    // add cards to correct zones by specifying type
    public void InsertCard(GameObject card, string type)
    {
        List<GameObject> cardList;
        switch (type)
        {
            case "unit":
                cardList = unitObjects;
                break;
            case "art":
                cardList = vaObjects;
                break;
            default:
                break;
        }

        if (card.CompareTag("PlayerCard") && Input.GetMouseButton(0) == false)
        {
            placementIndicator.SetActive(false);

            //change status of card and add to inPlay
            if (card.GetComponent<CardStatus>().status != "inPlay")
            {
                if (unitObjects.Count == 0)
                {
                    unitObjects.Add(card);
                }
                //add to cardObject list after checking relative location
                else
                {

                    for (int i = 0; i < unitObjects.Count; i++)
                    {
                        //when lower than 0, just insert at 0
                        if (i == 0 && card.gameObject.transform.position.x < unitObjects[i].transform.position.x)
                        {
                                unitObjects.Insert(0, card.gameObject);
                        }
                        //when higher than top of list, add gameobject
                        if (i == unitObjects.Count - 1 && card.gameObject.transform.position.x > unitObjects[i].transform.position.x)
                        {
                                unitObjects.Add(card.gameObject);
                        }
                        if (i != unitObjects.Count -1)
                        {
                            if (card.gameObject.transform.position.x > unitObjects[i].transform.position.x && card.gameObject.transform.position.x < unitObjects[i+1].transform.position.x)
                            {

                                unitObjects.Insert(i+1, card.gameObject);
                            }
                        }
                    }
                }
                //cardObjects.Add(card.gameObject);
                //once played, add card to inPlay list
                card.GetComponent<CardStatus>().status = "inPlay";
                inPlay.Add(card.GetComponent<CardDisplay>().card);
                card.transform.parent.GetComponent<HandDisplay>().hand.Remove(card.GetComponent<CardDisplay>().card);
            }
            //return to original color
            renderer.color = original;
            // reset rotation once card is played
            card.transform.rotation = Quaternion.Euler(0, 0, 0);

            ////set x positions
            //PositionCards(card.gameObject, unitStep, unitObjects.Count);
            ////set x positions
            //DisplayCards(PositionCards(cardTemplate, unitStep, unitObjects.Count), unitObjects);

            ////set y position for cards in play
            //    card.transform.position = new Vector3(card.transform.position.x, centerY1, card.transform.position.z);

            ////scales down in play cards according to public variable newScale
            //scale = card.transform.localScale;
            //scale *= .7f;
            //scale.x = Mathf.Clamp(scale.x, newScale, scale.x);
            //scale.y = Mathf.Clamp(scale.y, newScale, scale.y);
            //scale.z = Mathf.Clamp(scale.z, newScale, scale.z);

            //card.transform.localScale = scale;

            ////Logs variable representing the width of a card
            //Debug.Log(card.GetComponent<SpriteRenderer>().bounds.size.x);

            //adding comment for git test
            //second test
        }
    }

}

