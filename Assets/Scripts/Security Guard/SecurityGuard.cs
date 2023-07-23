using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class SecurityGuard : MonoBehaviour
{
    [Header("Private Variables")]
    private GameObject Player;
    //[HideInInspector]
    public List<GameObject> WalkPoints = new();
    private GameObject SecurityGuardWalkPointsParent;
    public int currentWP = 0;
    private float accuracyWP = 0.1f;
    private NavMeshAgent navmeshagent;

    [Header("Animator Parameters")]
    private Animator anim;
    private readonly string isRunning = "IsRunning";

    public GameObject smr;
    void Start()
    {
        //Search for the Player with Player tag.
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Hatem: Can't find the player, Check if you put tag 'Player' on the Player");
        }

        //Search for the SecurityGuardWalkPointsParent with SecurityGuardWalkPointsParent tag.
        SecurityGuardWalkPointsParent = GameObject.FindGameObjectWithTag("SecurityGuardWalkPointsParent");

        if (SecurityGuardWalkPointsParent == null)
        {
            Debug.LogError("Hatem: Can't find the walkpointsparent, Check if you put tag 'WalkPoints' on the WalkPointsParent");
        }

        //When the SecurityGuardWalkPointsParent are found add each child of the SecurityGuardWalkPointsParent to the list and those are the walkpoints of the security guard.
        //foreach (Transform child in SecurityGuardWalkPointsParent.transform)
        //{
        //    WalkPoints.Add(child.gameObject);
        //}

        //Search for the animator attached.
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Hatem: Cant find animator attached");
        }

        //Search for thenavmeshagent attached.
        navmeshagent = GetComponent<NavMeshAgent>();
        if (navmeshagent == null)
        {
            Debug.LogError("Hatem: Can't find navmeshagent attached");
        }
    }

    private void OnEnable()
    {
        Tutorial.instance.tutorialDone += SecurityGuardStatus;
        GamePlayManager.instance.LevelFinished += StopSecurityGuard;
    }

    private void OnDisable()
    {
        Tutorial.instance.tutorialDone -= SecurityGuardStatus;
        GamePlayManager.instance.LevelFinished += StopSecurityGuard;
    }

    private void StopSecurityGuard()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        StopAllCoroutines();
    }

    public void SecurityGuardStatus()
    {
        StartCoroutine(MoveGuard(currentWP));
    }

    private IEnumerator MoveGuard(int wayPoint)
    {
        yield return new WaitForFixedUpdate();

        navmeshagent.SetDestination(new Vector3(WalkPoints[wayPoint].transform.position.x, 0f, WalkPoints[wayPoint].transform.position.z));

        yield return new WaitForFixedUpdate();

        //Debug.Log(navmeshagent.remainingDistance);

        while (navmeshagent.remainingDistance > accuracyWP)
        {
            yield return null;
        }

        StartCoroutine(ChangeWayPoint());
    }

    private IEnumerator ChangeWayPoint()
    {
        //Change the waypoint of the security guard.
        currentWP++;
        //If the security guard reached his last walkpoint, resets walkpooints to 0 to restart his routine.
        if (currentWP >= WalkPoints.Count)
        {
            currentWP = 0;
        }
        yield return null;

        StopAllCoroutines();

        StartCoroutine(MoveGuard(currentWP));
    }

    public IEnumerator CatchPlayer()
    {
        navmeshagent.SetDestination(Player.transform.position);
        navmeshagent.speed = 4;
        anim.SetBool(isRunning, true);


        while (navmeshagent.remainingDistance > accuracyWP + 0.8f && gameObject.activeSelf)
        {
            yield return null;
        }

        StopAllCoroutines();

        GamePlayManager.instance.SecurityGuardReachedPlayer();
    }

    public void PersuitPlayer()
    {
        StartCoroutine(CatchPlayer());
    }
}
