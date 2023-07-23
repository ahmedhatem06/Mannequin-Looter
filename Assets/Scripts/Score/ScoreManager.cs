using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private ItemsInScene itemsInScene;
    public bool canComplete = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        itemsInScene = GameObject.FindGameObjectWithTag("ItemsInScene").GetComponent<ItemsInScene>();
        if (!itemsInScene)
        {
            Debug.LogError("Hatem: Can't find itemsInScene , Check if you put tag 'ItemsInScene' on itemsInScene");
        }
    }

    public bool CanCompleteLevel()
    {
        float percentage = ((float)itemsInScene.itemsCollected / (float)itemsInScene.allItemsCounter) * 100;

        if (percentage == 0 || percentage > 0 && percentage < 70)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int CalculateStars()
    {
        float percentage = ((float)itemsInScene.itemsCollected / (float)itemsInScene.allItemsCounter) * 100;

        if (percentage == 100)
        {
            return 3;
            //Debug.Log("3 Stars");
            //canComplete = true;
        }
        else if (percentage > 80 && percentage < 99.9)
        {
            return 2;

            //Debug.Log("2 Stars");
            //canComplete = true;
        }
        else if (percentage > 70 && percentage < 80)
        {
            return 1;

            //Debug.Log("1 Star");
            //canComplete = true;
        }
        else if (percentage == 0 || percentage > 0 && percentage < 70)
        {
            return 0;
            //Debug.Log("0 Stars");
            //canComplete = false;
        }

        return 0;
    }
}
