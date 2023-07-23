using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Animator Parameters")]
    [SerializeField]
    private string IsWalking = "IsWalking";
    [SerializeField]
    private string GotCaught = "GotCaught";
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Hatem: Cant find Animator attached");
        }
        animator.speed = 0;
    }

    public void PlayerMoving()
    {
        animator.speed = 1;
        animator.SetBool(IsWalking, true);
    }

    public void PlayerIdle()
    {
        animator.speed = 0;
    }

    public void PlayerLost()
    {
        animator.speed = 2;
        animator.SetBool(GotCaught, true);
    }
}
