using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    bool IsPlayerInside = true;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter" + other.name);
        if (other.tag == "Player")
        {
            IsPlayerInside = true;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit" + other.name);
        if (other.tag == "Player")
        {
            IsPlayerInside = false;
        }
    }

    public bool ReturnIsPlayerInside()
    {
        return IsPlayerInside;
    }

    public void CheckIfThePlayerIsInSafeZone()
    {
        if (!IsPlayerInside)
        {
            GamePlayManager.instance.CatchPlayer("You weren't at your spot!");
        }
    }
}
