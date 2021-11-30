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

    //distance between cards within group
    public float unitStep;

    //new scale in play, smaller than in hand
    public float newScale;

    public GameObject placementIndicator;
    public GameObject cardTemplate;

    //will determine whether to summon unit faceup or facedown
    public bool dragRight;
    private void Start()
    {
        //distance between cards in play
        unitStep = .04f;
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
        
        if (Input.GetMouseButton(1) == true)
        {
            dragRight = true;
        }
        else
        {
            dragRight = false;
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //visual feedback for card about to be played
        if (collision.CompareTag("PlayerCard"))
        {
            renderer.color = hoverColor;
            //Debug.Log("Collided");
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
        Card cardData = collision.gameObject.GetComponent<CardDisplay>().card;
        //card's type will determine which list to use
        string type = cardData.type;
        List<GameObject> cardList = TypeList(type);
        //final scale of card in play
        Vector3 scale;

        if (cardData.status == "inHand" && (Input.GetMouseButton(0) == true || Input.GetMouseButton(1) == true))
        {
            if (dragRight == true)
            {
                cardData.flipped = true;
            }    
            else
            {
                cardData.flipped = false;
            }
            SetIndicator(collision.gameObject, cardList, unitStep);
        }
        if (collision.CompareTag("PlayerCard") && (Input.GetMouseButton(0) == false && Input.GetMouseButton(1) == false ))
        {
            placementIndicator.SetActive(false);
            InsertCard(collision.gameObject, cardData.type, cardList);

            //once played, add card to inPlay list
            ChangeStatus(collision.gameObject);
            //return to original color
            renderer.color = original;
            //set x positions
            PositionCards(collision.gameObject, unitStep, unitObjects.Count);
            //set y positions, reset rotation, display
            DisplayCards(PositionCards(cardTemplate, unitStep, cardList.Count), cardList);
            //scales down in play cards according to public variable newScale
            ScaleCard(collision.gameObject);
            //Logs variable representing the width of a card
            if (cardData.flipped == true)
            {
                FlipCard(collision.gameObject);
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
        string type = "";
        float center;
        if (cards != null)
        {
            type = cards[0].GetComponent<CardDisplay>().card.type;
        }
        switch (type)
        {
            case "unit":
                center = centerY1;
                break;
            case "art":
                center = centerY2;
                break;
            default:
                center = 0;
                break;
        }
        
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = new Vector3(positions[i], center, cards[i].transform.position.z);
            cards[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void ScaleCard(GameObject card)
    {

        Vector3 scale;
        scale = card.transform.localScale;
        scale *= .7f;
        scale.x = Mathf.Clamp(scale.x, newScale, scale.x);
        scale.y = Mathf.Clamp(scale.y, newScale, scale.y);
        scale.z = Mathf.Clamp(scale.z, newScale, scale.z);

        card.transform.localScale = scale;
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
    public void InsertCard(GameObject card, string type, List<GameObject> cardList)
    {

        //Add to cardList after checking relative location
        if (card.GetComponent<CardStatus>().status != "inPlay")
        {
            if (cardList.Count == 0)
            {
                cardList.Add(card);
            }
            else
            {

                for (int i = 0; i < cardList.Count; i++)
                {
                    //when lower than 0, just insert at 0
                    if (i == 0 && card.transform.position.x < cardList[i].transform.position.x)
                    {
                            cardList.Insert(0, card.gameObject);
                    }
                    //when higher than top of list, add gameobject
                    if (i == cardList.Count - 1 && card.transform.position.x > cardList[i].transform.position.x)
                    {
                            cardList.Add(card.gameObject);
                    }
                    if (i != cardList.Count -1)
                    {
                        if (card.transform.position.x > cardList[i].transform.position.x && card.transform.position.x < cardList[i+1].transform.position.x)
                        {

                            cardList.Insert(i+1, card);
                        }
                    }
                }
            }
        }
    }
    public List<GameObject> TypeList(string type)
    {

        //Determine which list to insert to based on type
        switch (type)
        {
            case "unit":
                return unitObjects;
                break;
            case "art":
                return vaObjects;
                break;
            default:
                return new List<GameObject>();
                break;
        }
    }

    public void ChangeStatus(GameObject card)
    {

        card.GetComponent<CardStatus>().status = "inPlay";
        card.GetComponent<CardDisplay>().card.status = "inPlay";
        inPlay.Add(card.GetComponent<CardDisplay>().card);
        card.transform.parent.GetComponent<HandDisplay>().hand.Remove(card.GetComponent<CardDisplay>().card);
    }

    public void FlipCard(GameObject card)
    {
        Sprite sprite = card.GetComponent<CardDisplay>().frontCard;
        if (sprite == card.GetComponent<CardDisplay>().frontCard)
        {
            sprite = card.GetComponent<CardDisplay>().backCard;
            card.GetComponent<CardDisplay>().SetActiveAllChildren(card.transform, false);
        }
        else
        {
            sprite = card.GetComponent<CardDisplay>().frontCard;
            card.GetComponent<CardDisplay>().SetActiveAllChildren(card.transform, true);
        }
        card.GetComponent<SpriteRenderer>().sprite = sprite;
        
    }

}

