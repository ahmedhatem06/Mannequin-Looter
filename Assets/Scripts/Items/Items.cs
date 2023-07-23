using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Public Variables")]
    public float ItemRotationSpeed = 90f;
    public bool TakeIt = false;
    public float ItemMovementSpeed = 0.1f;
    public GameObject DollarsPE;
    public ItemsInScene itemsInScene;
    public bool canPickUp;
    public float scaleFactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetComponent<ItemsInScene>())
        {
            itemsInScene = transform.parent.GetComponent<ItemsInScene>();

            DollarsPE = itemsInScene.GetComponent<ItemsInScene>().dollars_PE;
        }

        //Add here the prerequisites to pick up the item.
        if (!GetComponentInChildren<GlassBreaker>())
        {
            canPickUp = true;
        }

    }

    public void itemCollected()
    {
        itemsInScene.PlayerHasCollectedAnItem();
    }

    public void OnTriggerStay(Collider other)
    {
        if (canPickUp)
        {
            if (GetComponent<NavMeshObstacle>() != null)
            {
                GetComponent<NavMeshObstacle>().enabled = false;
            }

            if (GetComponent<BoxCollider>() != null)
            {
                GetComponent<BoxCollider>().enabled = false;
            }

            if (GetComponent<CapsuleCollider>() != null)
            {
                GetComponent<CapsuleCollider>().enabled = false;
            }

            StartCoroutine(itemAnimation(other.gameObject));
            TakeIt = true;

            Pool.instance.ActivatePoolObject(transform.position);
            AudioManager.instance.PlaySoundEffect(1);


            itemCollected();

            VibrateDevice();
        }
    }

    private void VibrateDevice()
    {
        if (PlayerPrefs.GetString("HapticStatus") == "True")
        {
            //Checks for phone.
#if UNITY_IPHONE || UNITY_ANDROID
            Vibration.Vibrate(40);
#endif
        }
    }

    IEnumerator itemAnimation(GameObject other)
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * scaleFactor, transform.localScale.y - Time.deltaTime * scaleFactor, transform.localScale.z - Time.deltaTime * scaleFactor);
            transform.Rotate(Vector3.up * (ItemRotationSpeed * Time.deltaTime));
            transform.Rotate(Vector3.right * (ItemRotationSpeed * Time.deltaTime));
            transform.Rotate(Vector3.forward * (ItemRotationSpeed * Time.deltaTime));
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, ItemMovementSpeed);
            yield return null;
        }

        if (transform.localScale.x < 0)
        {
            Destroy(gameObject);
        }
    }

    public void CanPickUpItem()
    {
        StartCoroutine(CanPickUpAfterTime());
    }

    //In Glass Breaker the sound doesn't play to I had to make in in IEnumerator to wait some time.
    IEnumerator CanPickUpAfterTime()
    {
        yield return new WaitForSeconds(0.1f);
        canPickUp = true;
    }
}
