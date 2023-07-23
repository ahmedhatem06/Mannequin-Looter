using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameplayLoader : MonoBehaviour
{
    private string gameplaySceneName = "Gameplay";
    public void LoadLevel(Button button)
    {
        PlayerPrefs.SetInt("Level", int.Parse(button.name));
        SceneManager.LoadScene(gameplaySceneName);
    }
}
