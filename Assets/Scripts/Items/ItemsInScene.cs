using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInScene : MonoBehaviour
{
    [Header("Items Counter")]
    public int allItemsCounter = 0;

    [Header("Items that the Player has collected")]
    public int itemsCollected = 0;

    public GameObject dollars_PE;
    void Start()
    {
        allItemsCounter = transform.childCount;

        UIManager.instance.bagItems.ReceiveItemsCounter(allItemsCounter);
    }

    public void PlayerHasCollectedAnItem()
    {
        itemsCollected++;

        UIManager.instance.bagItems.itemCollected();

        if (itemsCollected == allItemsCounter)
        {
            Debug.Log("All items have been collected");
        }
    }
}
