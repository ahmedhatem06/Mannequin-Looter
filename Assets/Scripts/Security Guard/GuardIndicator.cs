using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardIndicator : MonoBehaviour
{
    private string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            if (GamePlayManager.instance.PlayerMovingStatus() && !GamePlayManager.instance.isPlayerCaught)
            {
                GamePlayManager.instance.CatchPlayer("You have been caught!");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == playerTag)
        {
            if (GamePlayManager.instance.PlayerMovingStatus() && !GamePlayManager.instance.isPlayerCaught)
            {
                GamePlayManager.instance.CatchPlayer("You have been caught!");
            }
        }
    }
}
