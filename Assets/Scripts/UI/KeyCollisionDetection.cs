using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisionDetection : MonoBehaviour
{
    public bool isRight;
    private DragUI dragUI;
    public LockPick lockPick;
    private void Start()
    {
        dragUI = GetComponent<DragUI>();
    }

    public void IsRight()
    {
        isRight = true;
    }

    public void IsFalse()
    {
        isRight = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRight)
        {
            lockPick.CorrectKey();
            gameObject.SetActive(false);
            dragUI.ResetPosition();
        }
        else
        {
            dragUI.ResetPosition();
        }
    }
}
