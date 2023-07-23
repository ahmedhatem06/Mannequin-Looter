using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WalkPointsLines : MonoBehaviour
{
#if UNITY_EDITOR
    public bool showLines = false;
    private List<Transform> walkPoints = new();
    void OnDrawGizmosSelected()
    {
        if (showLines)
        {
            foreach (Transform child in transform)
            {
                walkPoints.Add(child);
            }

            for (int i = 0; i < walkPoints.Count; i++)
            {
                if (i + 1 == walkPoints.Count)
                {
                    Handles.DrawLine(walkPoints[i].position, walkPoints[0].position);
                }
                else
                {
                    Handles.DrawLine(walkPoints[i].position, walkPoints[i + 1].position);
                }
            }
        }
    }
#endif
}
