using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public LockPick lockPick;
    private void OnEnable()
    {
        lockPick.lockOpened += OpenDoor;
    }

    private void OnDisable()
    {
        lockPick.lockOpened += OpenDoor;
    }

    private void OpenDoor(GameObject door, Quaternion quaternion)
    {
        StartCoroutine(RotateDoor(door, quaternion));
    }

    public IEnumerator RotateDoor(GameObject door, Quaternion targetTransform, float seconds = 0.5f)
    {
        float elapsedTime = 0;

        Quaternion originalQuaternion = door.transform.rotation;

        while (elapsedTime < seconds)
        {
            door.transform.rotation = Quaternion.Lerp(originalQuaternion, targetTransform, (elapsedTime / seconds));
            //door.transform.localPosition = Vector3.Lerp(originalTransform, targetTransform, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
