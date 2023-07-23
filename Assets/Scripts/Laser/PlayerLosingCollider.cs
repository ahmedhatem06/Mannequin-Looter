using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLosingCollider : MonoBehaviour
{
    private string playerTag = "Player";
    private string losingText = "You got caught in the lasers!";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (GamePlayManager.instance.PlayerMovingStatus())
            {
                GamePlayManager.instance.CatchPlayer(losingText);
            }
        }
    }
}
