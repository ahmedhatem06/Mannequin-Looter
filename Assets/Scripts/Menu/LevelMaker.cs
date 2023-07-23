using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    public GameObject levelButtonPrefab;
    public int numberOfLevels;

    void Start()
    {
        for (int i = 0; i < numberOfLevels; i++)
        {
            GameObject currentButton = Instantiate(levelButtonPrefab, transform.position, Quaternion.identity);
            currentButton.transform.SetParent(transform);
            currentButton.name = i.ToString();
            currentButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }

}
