using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ManualDoor : MonoBehaviour
{
    public KeyLock keyLock;
    //TODO: Change all playerTag in other scripts to readonly.
    private readonly string playerTag = "Player";
    private Canvas breakLoadingCanvas;
    private Slider glassBreakerSlider;
    private bool startTimer;
    private float timer;
    private bool isDoorOpened = false;

    //Key.
    private bool canOpenDoor = false;
    public BoxCollider middleWallBoxCollider;
    public GameObject rightDoor;
    public Quaternion rightDoorQuaternion;
    public GameObject leftDoor;
    public Quaternion leftDoorQuaternion;
    private BoxCollider boxCollider;
    public event Action<GameObject, Quaternion> FinishedDoorLoading;
    private void OnEnable()
    {
        keyLock.keyFound += FoundKey;
    }

    private void OnDisable()
    {
        keyLock.keyFound -= FoundKey;
    }

    private void FoundKey()
    {
        canOpenDoor = true;
    }

    private void Start()
    {
        breakLoadingCanvas = GetComponent<Canvas>();
        glassBreakerSlider = GetComponentInChildren<Slider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && canOpenDoor)
        {
            OpenDoorTrigger(true);
        }
        else
        {
            Debug.Log("Key is not found yet");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag) && !isDoorOpened)
        {
            if (startTimer)
            {
                if (timer > glassBreakerSlider.maxValue)
                {
                    FinishedDoorLoading?.Invoke(rightDoor, rightDoorQuaternion);
                    FinishedDoorLoading?.Invoke(leftDoor, leftDoorQuaternion);

                    boxCollider.enabled = false;
                    middleWallBoxCollider.enabled = false;

                    //TODO: Add door sound effect.
                    AudioManager.instance.PlaySoundEffect(2);

                    isDoorOpened = true;
                    OpenDoorTrigger(false);
                }
                timer += Time.deltaTime;
                glassBreakerSlider.value = timer;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && canOpenDoor)
        {
            OpenDoorTrigger(false);
        }
    }

    private void OpenDoorTrigger(bool status)
    {
        startTimer = status;
        breakLoadingCanvas.enabled = status;
        timer = 0;
    }
}

