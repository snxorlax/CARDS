using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBehavior : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Color originalColor;
    public Transform cardTransform;
    public Vector3 originalScale;
    public Color newColor;
    public float alpha;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        cardTransform = GetComponent<Transform>();
        originalColor = renderer.color;
        newColor = originalColor;
        newColor.a *= alpha;

    }
    private void Start()
    {
        originalScale = cardTransform.localScale;
    }
    private void OnMouseEnter()
    {
        renderer.color = newColor;
        cardTransform.localScale *= 1.2f;
    }
    private void OnMouseExit()
    {
        renderer.color = originalColor;
        cardTransform.localScale = originalScale;
    }

}
