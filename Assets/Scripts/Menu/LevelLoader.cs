using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject Managers;
    public GameObject Canvases;
    [SerializeField]
    public EnvironmentLink[] environmentLink;

    private GameObject levelPrefab;
    private void Awake()
    {
        LoadLevelPrefab();

        LoadEnvironmentPrefab();
    }

    private void Start()
    {
        Managers.SetActive(true);
        Canvases.SetActive(true);
    }

    public void LoadLevelPrefab()
    {
        levelPrefab = Resources.Load<GameObject>("Levels Prefabs" + "/" + PlayerPrefs.GetInt("Level"));
        GameObject currentLevel = Instantiate(levelPrefab, transform.position, levelPrefab.transform.rotation);
        currentLevel.name = "Level " + PlayerPrefs.GetInt("Level");
    }

    public void LoadEnvironmentPrefab()
    {
        if (levelPrefab.GetComponent<BelongEnvironment>())
        {
            for (int i = 0; i < environmentLink.Length; i++)
            {
                if (environmentLink[i].environment == levelPrefab.GetComponent<BelongEnvironment>().environment)
                {
                    GameObject environmentPrefab = Instantiate(environmentLink[i].environmentPrefab);
                    environmentPrefab.name = environmentLink[i].environmentPrefab.name;
                }
            }
        }
    }
}

[Serializable]
public class EnvironmentLink
{
    public Environment environment;
    public GameObject environmentPrefab;
}
[Serializable]

public enum Environment
{
    Park,
    Bank,
    Office
}