using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float deltaY;
    public Vector3 newPos;
    public float maxHeight, minHeight;
    private void Update()
    {
        deltaY = Input.mouseScrollDelta.y;
        float newY = transform.position.y + deltaY;
        newY = Mathf.Clamp(newY, minHeight, maxHeight);
        newPos = new Vector3(transform.position.x, newY, transform.position.z);
        transform.position = newPos;
    }
}
