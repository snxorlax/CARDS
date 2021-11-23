using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTurn : MonoBehaviour
{
    public GameObject endText;
    public Vector3 originalScale;
    public Color originalColor;
    public TextMeshProUGUI text;

    private void Start()
    {
        originalColor = text.color;
        originalScale = endText.transform.localScale;
    }
    public void PassTurn()
    {
        
        endText.SetActive(true);
        StartCoroutine(AnimateText());
        endText.transform.localScale = originalScale;

    }

    public IEnumerator AnimateText()
    {
        float a = originalColor.a;
        for (int i = 0; i < 120; i++)
        {
            endText.transform.localScale *= 1.004f;
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
        endText.SetActive(false);


    }


}
