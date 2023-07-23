using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int level;
    private string gameplaySceneName = "Gameplay";

    //This is only for testing and works only in editor.
    void Start()
    {
        if (Application.isEditor)
        {
            if (gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("Level", level);
                SceneManager.LoadScene(gameplaySceneName);
            }
        }
    }
}
