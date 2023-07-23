using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Material redMaterial;
    public Material greenMaterial;
    public GameObject rightDoor;
    public GameObject leftDoor;
    public MeshRenderer scannerReader;
    private string playerTag = "Player";
    private Vector3 originalRightDoorTransform;
    private Vector3 originalLeftDoorTransform;
    private Vector3 targetRightDoorTransform;
    private Vector3 targetLeftDoorTransform;

    //[SerializeField] private float rightDoorFactor;
    //[SerializeField] private float leftDoorFactor;

    private void Start()
    {
        originalRightDoorTransform = rightDoor.transform.localPosition;
        originalLeftDoorTransform = leftDoor.transform.localPosition;
        targetRightDoorTransform = new(rightDoor.transform.localPosition.x + 1.3f, rightDoor.transform.localPosition.y, rightDoor.transform.localPosition.z);
        targetLeftDoorTransform = new(leftDoor.transform.localPosition.x - 1.3f, leftDoor.transform.localPosition.y, leftDoor.transform.localPosition.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ChangeScannerMaterial(greenMaterial);
            StopAllCoroutines();
            StartCoroutine(MoveDoor(rightDoor, originalRightDoorTransform, targetRightDoorTransform));
            StartCoroutine(MoveDoor(leftDoor, originalLeftDoorTransform, targetLeftDoorTransform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ChangeScannerMaterial(redMaterial);
            StopAllCoroutines();
            StartCoroutine(MoveDoor(rightDoor, targetRightDoorTransform, originalRightDoorTransform));
            StartCoroutine(MoveDoor(leftDoor, targetLeftDoorTransform, originalLeftDoorTransform));
        }
    }

    //TODO: Refactor this function to remove unused parameter (originalTransform).
    public IEnumerator MoveDoor(GameObject door, Vector3 originalTransform, Vector3 targetTransform, float seconds = 0.5f)
    {
        float elapsedTime = 0;

        originalTransform = door.transform.localPosition;

        while (elapsedTime < seconds)
        {
            door.transform.localPosition = Vector3.Lerp(originalTransform, targetTransform, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void ChangeScannerMaterial(Material scannerMaterial)
    {
        scannerReader.material = scannerMaterial;
    }
}
