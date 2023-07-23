using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndButtons : MonoBehaviour
{
    private string gameplaySceneName = "Gameplay";
    public void RestartLevel()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void NextLevel()
    {
        int levelNumber = int.Parse(PlayerPrefs.GetInt("Level").ToString());
        levelNumber++;
        PlayerPrefs.SetInt("Level", levelNumber);

        if (CheckCurrentLevel())
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        else
        {
            GamePlayManager.instance.levelEnd.CloseWinScreen();
            GamePlayManager.instance.levelEnd.GetComponent<LevelEndAnimationManager>().StartEndingAnimation();
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene(gameplaySceneName);
    }

    private bool CheckCurrentLevel()
    {
        GameObject tmplevelPrefab = Resources.Load<GameObject>("Levels Prefabs" + "/" + PlayerPrefs.GetInt("Level"));
        if (tmplevelPrefab)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
