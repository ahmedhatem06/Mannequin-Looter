using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlassBreaker : MonoBehaviour
{
    public GameObject normalGlass;
    public GameObject brokenGlass;
    private string playerTag = "Player";
    private Canvas breakLoadingCanvas;
    private Slider glassBreakerSlider;
    private bool startTimer;
    private float timer;
    private bool isGlassBroken = false;
    public BoxCollider additionalBoxCollider;
    private void Start()
    {
        breakLoadingCanvas = GetComponent<Canvas>();
        glassBreakerSlider = GetComponentInChildren<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isGlassBroken)
        {
            BreakGlassTrigger(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag) && !isGlassBroken)
        {
            if (startTimer)
            {
                if (timer > glassBreakerSlider.maxValue)
                {
                    if (additionalBoxCollider)
                    {
                        //additionalBoxCollider.enabled = false;
                    }

                    if (brokenGlass)
                    {
                        brokenGlass.SetActive(true);
                    }

                    if (normalGlass)
                    {
                        normalGlass.SetActive(false);
                    }

                    AudioManager.instance.PlaySoundEffect(2);

                    isGlassBroken = true;
                    BreakGlassTrigger(false);

                    if(GetComponentInParent<Items>())
                    {
                        GetComponentInParent<Items>().CanPickUpItem();
                    }
                }
                timer += Time.deltaTime;
                glassBreakerSlider.value = timer;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && !isGlassBroken)
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
