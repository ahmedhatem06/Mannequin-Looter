using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItems : MonoBehaviour
{
    public TMPro.TextMeshProUGUI itemsCounterText;
    private int allItemsCounter = 0;
    private int itemsCollected = 0;

    public void ReceiveItemsCounter(int counter)
    {
        allItemsCounter = counter;

        UpdateItemsCounter();
    }

    public void itemCollected()
    {
        itemsCollected++;

        UpdateItemsCounter();
    }

    private void UpdateItemsCounter()
    {
        itemsCounterText.text = itemsCollected + "/ " + allItemsCounter;
    }
}
