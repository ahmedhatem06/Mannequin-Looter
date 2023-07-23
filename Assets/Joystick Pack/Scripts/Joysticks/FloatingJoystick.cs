using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : Joystick
{
    private bool canMove = false;
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        Tutorial.instance.tutorialDone += ResumeJoystick;
        UIManager.instance.uiSettings.PauseTheGame += PauseJoystick;
        UIManager.instance.uiSettings.ResumeTheGame += ResumeJoystick;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (canMove)
        {
            background.localPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (canMove)
        {
            background.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
        }
    }

    private void ResumeJoystick()
    {
        canMove = true;
        GetComponent<Image>().raycastTarget = true;
    }

    private void PauseJoystick()
    {
        canMove = false;
        GetComponent<Image>().raycastTarget = false;
    }
}