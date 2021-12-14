using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayEvent : MonoBehaviour
{
    public GameObject eventText;
    public Vector3 originalScale;
    public Color originalColor;
    public TextMeshProUGUI text;

    private void Awake()
    {
        eventText = transform.GetChild(0).gameObject;
        text = eventText.GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
        originalScale = eventText.transform.localScale;
        GameManager.GameStarted += () => EventText("Game Started");
        GameManager.OnTurnStarted += () => EventText("Turn Started");
        GameManager.OnDrawStarted += () => EventText("Draw");
        GameManager.OnMainStarted += () => EventText("Main Phase");
        GameManager.OnTurnEnded += () => EventText("Turn Ended");
    }
    public void EventText(string text)
    {

        eventText.transform.localScale = originalScale;
        eventText.SetActive(true);
        ChangeEventText(text);
        StartCoroutine(AnimateText());

    }

    public IEnumerator AnimateText()
    {
        float a = originalColor.a;
        for (int i = 0; i < 120; i++)
        {
            eventText.transform.localScale *= 1.004f;
            if (a >= 1)
            {
                a -= .02f;
            }
            else
            {
                a += .02f;
            }
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
            yield return null;
        }

        yield return new WaitForSeconds(1.1f);
        eventText.SetActive(false);


    }
    public void ChangeEventText(string newEventText)
    {
        text.text = newEventText;
    }


}
