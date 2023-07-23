using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Public Variables")]
    public float PlayerMovementSpeed = 0.05f;

    [Header("Private Variables")]
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float singlestep = 0f;
    [SerializeField]
    private float playerrotationspeed = 1.0f;
    public bool isPlayerMoving = false;

    private Rigidbody playerRigidbody;
    private RigidbodyConstraints playerDefaultRigidbodyConstraints;
    private RigidbodyConstraints playerIdleRigidbodyConstraints;

    //Scripts.
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public PlayerMovementAndRotation playerMovementAndRotation;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Hatem: Can't find Camera attached");
        }

        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Hatem: Cant find Rigidbody attached");
        }
        playerDefaultRigidbodyConstraints = playerRigidbody.constraints;
        playerIdleRigidbodyConstraints = RigidbodyConstraints.FreezeAll;

        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovementAndRotation = GetComponent<PlayerMovementAndRotation>();

        Tutorial.instance.tutorialDone += SecurityGuardStatus;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (CanMove == true)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            //ChangeRigidbodyFreezeStatus(true);

    //            //PlayerMovementAndRotation();
    //        }

    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            //animator.speed = 0;
    //            //isPlayerMoving = false;

    //            //ChangeRigidbodyFreezeStatus(false);
    //        }
    //    }
    //}

    public void SecurityGuardStatus()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        //CanMove = true;
    }

    //private void PlayerMovementAndRotation()
    //{
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        if (!hit.transform.CompareTag("Player") && !UIManager.instance.uiSettings.isSettingsOpen)
    //        {
    //            PlayerAnimation();

    //            isPlayerMoving = true;

    //            Vector3 target = new(hit.point.x, 0, hit.point.z);

    //            PlayerRotation(target);

    //            PlayerMovement(target);
    //        }
    //    }
    //}

    //private void PlayerRotation(Vector3 Target)
    //{
    //    Vector3 Direction = Target - transform.position;
    //    singlestep = playerrotationspeed * Time.deltaTime;
    //    Vector3 newDirection = Vector3.RotateTowards(transform.forward, Direction, singlestep, 0.0f);

    //    // Draw a ray pointing at our target in
    //    //Debug.DrawRay(transform.position, newDirection, Color.red);

    //    // Calculate a rotation a step closer to the target and applies rotation to this object
    //    transform.rotation = Quaternion.LookRotation(newDirection);
    //}

    //private void PlayerMovement(Vector3 target)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, target, PlayerMovementSpeed * Time.deltaTime);
    //}

    //private void PlayerAnimation()
    //{
    //    animator.speed = 1;
    //    animator.SetBool(IsWalking, true);
    //}



    //private void ChangeRigidbodyFreezeStatus(bool status)
    //{
    //    if (status)
    //    {
    //        playerRigidbody.constraints = playerDefaultRigidbodyConstraints;
    //    }
    //    else
    //    {
    //        playerRigidbody.constraints = playerIdleRigidbodyConstraints;
    //    }
    //}

    public void Lost()
    {
        GetComponent<PlayerMovementAndRotation>().PlayerCantMove();
        GetComponent<PlayerAnimation>().PlayerLost();
    }
}
