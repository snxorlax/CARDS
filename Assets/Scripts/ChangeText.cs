using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public string text;
    private void Start()
    {
        GameManager.OnTurnStarted += () => ChangeEventText("Turn Start");

    }
    public void ChangeEventText(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }

}
