using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndAnimationManager : MonoBehaviour
{
    public UIAnimation[] winUIAnimations;
    public UIAnimation[] loseUIAnimations;    
    public UIAnimation[] endUIAnimations;
    public void StartWinningAnimation()
    {
        gameObject.SetActive(true);

        StartCoroutine(StartWinAnimation());
    }

    private IEnumerator StartWinAnimation()
    {
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < winUIAnimations.Length; i++)
        {
            winUIAnimations[i].StartAnimation();
        }
    }

    public void StartLosingAnimation()
    {
        gameObject.SetActive(true);

        StartCoroutine(StartLoseAnimation());
    }

    private IEnumerator StartLoseAnimation()
    {
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < loseUIAnimations.Length; i++)
        {
            loseUIAnimations[i].StartAnimation();
        }
    }

    public void StartEndingAnimation()
    {
        gameObject.SetActive(true);

        StartCoroutine(StartEndAnimation());
    }

    private IEnumerator StartEndAnimation()
    {
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < endUIAnimations.Length; i++)
        {
            endUIAnimations[i].StartAnimation();
        }
    }
}