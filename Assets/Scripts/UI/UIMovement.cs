using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    private RectTransform rectTransform;
    float time = 0;
    float maxTime = 2;
    public float speed = 2;
    bool isRight = false;
    int switchCounter = 0;
    Vector2 targetRightPosition;
    Vector2 targetLeftPosition;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetRightPosition = new(200, rectTransform.anchoredPosition.y);
        targetLeftPosition = new(-200, rectTransform.anchoredPosition.y);
        MoveSwitcher();
        Tutorial.instance.tutorialDone += StopMoving;
    }

    private void StopMoving()
    {
        StopAllCoroutines();
    }

    public IEnumerator MoveOverSeconds(Vector2 target, float seconds = 2f)
    {
        float elapsedTime = 0;
        Vector2 startingPos = rectTransform.anchoredPosition;
        while (elapsedTime < seconds)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startingPos, target, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //rectTransform.anchoredPosition = new(rectTransform.anchoredPosition.x + xPosition, rectTransform.anchoredPosition.y);
        MoveSwitcher();
    }

    public void MoveSwitcher()
    {
        if (switchCounter % 2 == 0)
        {
            StartCoroutine(MoveOverSeconds(targetRightPosition));
        }
        else
        {
            StartCoroutine(MoveOverSeconds(targetLeftPosition));
        }
        switchCounter++;
    }
}
