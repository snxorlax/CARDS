using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttackable : MonoBehaviour, IDropHandler
{
    public GameObject enemy;
    private void Start()
    {
        enemy = transform.parent.gameObject;
    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        GameObject activeObject = pointerEventData.pointerDrag;
        if (activeObject != null && activeObject.CompareTag("PlayerCard") && activeObject.GetComponent<CardStatus>().status == "inPlay")
        {
            enemy.GetComponent<PlayerDisplay>().player.hp -= 1;
        }
    }
}
