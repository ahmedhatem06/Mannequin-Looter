using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBox : MonoBehaviour
{
    public GameObject[] lasers;
    private int lasersReminder = 0;
    public float time;
    private void Start()
    {
        Finished();
    }

    private IEnumerator OpenAndCloseAfterTime()
    {
        yield return new WaitForSeconds(time);

        if (lasersReminder % 2 == 0)
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].SetActive(true);
            }
        }

        lasersReminder++;

        Finished();
    }

    public void Finished()
    {
        StartCoroutine(OpenAndCloseAfterTime());
    }
}
