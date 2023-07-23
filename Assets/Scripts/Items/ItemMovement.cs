using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public MovementType movementType;
    public float speed = 3;
    private Vector3 startPos;
    public float distance = 0.005f;
    private void Start()
    {
        startPos = transform.position;

        StartCoroutine(MoveItem());
    }

    IEnumerator MoveItem()
    {
        while (movementType == MovementType.RightAndLeft)
        {
            Vector3 v = startPos;
            startPos.x += distance * Mathf.Sin(Time.time * speed);
            transform.position = v;
            yield return null;
        }

        while (movementType == MovementType.UpAndDown)
        {
            Vector3 v = startPos;
            startPos.y += distance * Mathf.Sin(Time.time * speed);
            transform.position = v;
            yield return null;
        }

    }
}
public enum MovementType
{
    RightAndLeft,
    UpAndDown
}