using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildDisplay : MonoBehaviour
{
    public List<Card> ownedCards;
    public GameObject cardTemplate;

    //spacing variables
    public float stepHorizontal;
    public float stepVertical;
    public Vector3 startPos;
    public float cardWidth;
    public float cardHeight;
    public int lastRow;
    public Vector3 newScale;


    private void Start()
    {
        int columns = 5;
        int rows = ownedCards.Count / columns;
        lastRow = ownedCards.Count % columns;
        cardWidth = cardTemplate.GetComponent<SpriteRenderer>().bounds.size.x;
        cardHeight = cardTemplate.GetComponent<SpriteRenderer>().bounds.size.y;

        DisplayCards(rows, columns);
    }
    //display cards to be added to deck
    public void DisplayCards(int rows, int columns)
    {
        float xDis;
        float yDis;
        int counter = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                xDis = ((stepHorizontal + cardWidth) * j);
                yDis = ((stepVertical + cardHeight) * i);
                Vector3 newPos = new Vector3(startPos.x + xDis, startPos.y - yDis, startPos.z);
                GameObject card = Instantiate(cardTemplate, newPos, Quaternion.identity, transform.parent);
                card.GetComponent<CardDisplay>().card = ownedCards[counter];
                counter++;
                card.transform.localScale = newScale;
                card.GetComponent<SpriteRenderer>().sortingOrder = 0;
                card.GetComponent<CardDisplay>().card.status = "deckBuild";
            }
        }
        if (lastRow != 0)
        {
            yDis = ((stepVertical + cardHeight) * rows);
            for (int i = 0; i < lastRow; i++)
            {
                xDis = ((stepHorizontal + cardWidth) * i);
                Vector3 newPos = new Vector3(startPos.x + xDis, startPos.y - yDis, startPos.z);
                GameObject card = Instantiate(cardTemplate, newPos, Quaternion.identity, transform.parent);
                card.GetComponent<CardDisplay>().card = Instantiate(ownedCards[ownedCards.Count - lastRow + i]);
                card.transform.localScale = newScale;
                card.GetComponent<SpriteRenderer>().sortingOrder = 0;
                card.GetComponent<CardDisplay>().card.status = "deckBuild";
            }
        }
    }
}
