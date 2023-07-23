using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public event Action PlayerFinished;

    private void Start()
    {
        GamePlayManager.instance.carManager = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFinished?.Invoke();
        }
    }
}
