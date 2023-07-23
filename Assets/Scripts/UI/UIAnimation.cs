using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    private Vector3 maxTargetScale;
    private Vector3 targetScale;
    [SerializeField] private float waitTime;
    void Start()
    {
        targetScale = new(1, 1, 1);
        maxTargetScale = new(1.2f, 1.2f, 1.2f);
    }

    public void StartAnimation()
    {
        StartCoroutine(AnimateUI(maxTargetScale, targetScale));
    }

    public IEnumerator AnimateUI(Vector3 target, Vector3 maxTarget, float duration = 0.4f)
    {
        yield return new WaitForSeconds(waitTime);

        Vector3 originalScale = transform.localScale;

        yield return AnimateScale(originalScale, target, duration);
        yield return AnimateScale(target, maxTarget, 0.1f);
    }

    private IEnumerator AnimateScale(Vector3 startScale, Vector3 endScale, float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }
}
