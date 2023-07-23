using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    public int width;
    public int height;
    private void Start()
    {
        Screen.SetResolution(width, height,true);
    }
}
