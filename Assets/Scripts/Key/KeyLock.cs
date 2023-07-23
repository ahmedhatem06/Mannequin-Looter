using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    private string playerTag = "Player";
    public event Action keyFound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            keyFound?.Invoke();
        }
    }
}
