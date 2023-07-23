using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    [SerializeField] LockMap[] lockMap;
    [SerializeField] Image targetKey;
    [SerializeField] Image lockImage;
    [SerializeField] Sprite openLock;
    public BoxCollider gateCollider;
    public Quaternion gateDoorQuaternion;
    public event System.Action<GameObject, Quaternion> lockOpened;
    private bool isLockOpened;
    private void Start()
    {
        int randomNumber = Random.Range(0, lockMap.Length);
        targetKey.sprite = lockMap[randomNumber].smallKey;
        for (int i = 0; i < lockMap.Length; i++)
        {
            if (i == randomNumber)
            {
                lockMap[i].key.GetComponent<KeyCollisionDetection>().IsRight();
            }
            else
            {
                lockMap[i].key.GetComponent<KeyCollisionDetection>().IsFalse();
            }
        }
    }

    public void CorrectKey()
    {
        lockImage.sprite = openLock;
        lockOpened?.Invoke(gateCollider.gameObject, gateDoorQuaternion);

        StartCoroutine(CloseCanvasAfterTime());
        isLockOpened = true;
    }

    public bool LockStatus()
    {
        return isLockOpened;
    }

    private IEnumerator CloseCanvasAfterTime()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    [System.Serializable]
    public class LockMap
    {
        public Sprite smallKey;
        public Sprite fullKey;
        public GameObject key;
    }

    public void OpenLockPick()
    {
        gameObject.SetActive(true);
    }

    public void CloseLockPick()
    {
        gameObject.SetActive(false);
    }
}
