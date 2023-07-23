using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockBreaker : MonoBehaviour
{
    public LockPick lockPick;
    public GameObject lockPickCanvas;
    private readonly string playerTag = "Player";
    private Canvas breakLoadingCanvas;
    private Slider glassBreakerSlider;
    private bool startTimer;
    private float timer;

    private void Start()
    {
        breakLoadingCanvas = GetComponent<Canvas>();
        glassBreakerSlider = GetComponentInChildren<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !lockPick.LockStatus())
        {
            BreakGlassTrigger(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag) && !lockPick.LockStatus())
        {
            if (startTimer)
            {
                if (timer > glassBreakerSlider.maxValue)
                {
                    lockPickCanvas.SetActive(true);
                    //isGlassBroken = true;
                    BreakGlassTrigger(false);
                }
                timer += Time.deltaTime;
                glassBreakerSlider.value = timer;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && !lockPick.LockStatus())
        {
            BreakGlassTrigger(false);
        }
    }

    private void BreakGlassTrigger(bool status)
    {
        startTimer = status;
        breakLoadingCanvas.enabled = status;
        timer = 0;
    }
}
