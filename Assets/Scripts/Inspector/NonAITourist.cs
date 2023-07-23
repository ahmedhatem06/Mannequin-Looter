using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NonAITourist : MonoBehaviour
{
    [Header("Public Variables")]
    public List<GameObject> WalkPoints = new();
    public List<GameObject> LookPoints = new();
    [HideInInspector]
    public GameObject playerZone;
    public float movementSpeed = 1f;
    [HideInInspector]
    public TMPro.TextMeshProUGUI TimerText;
    [HideInInspector]
    public GameObject smr;
    [HideInInspector]
    public GameObject selectedSMR;

    [Header("Animator Properties")]
    private Animator animator;
    private readonly string isWalking = "IsWalking";
    private readonly string isInspecting = "IsInspecting";

    [Header("Private Variables")]
    private int currentWP = 0;
    private float accuracyWP = 0.1f;
    private float timeofInspecting = 10f;
    private float timeLeft = 0;
    private bool beginTimerTextCountDown = true;
    private float time = 0;

    public void Start()
    {
        //Search for the animator attached.
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Hatem: Cant find animator attached");
        }

        playerZone = GameObject.FindGameObjectWithTag("PlayerStartPosition");
        if (playerZone == null)
        {
            Debug.LogError("Hatem: Can't find the playerZone, Check if you put tag 'playerStartPosition' on the PlayerZone");
        }

        GamePlayManager.instance.LevelFinished += StopInspector;
    }

    private void OnEnable()
    {
        Tutorial.instance.tutorialDone += SecurityGuardStatus;
    }

    private void OnDisable()
    {
        Tutorial.instance.tutorialDone -= SecurityGuardStatus;
        GamePlayManager.instance.LevelFinished -= StopInspector;
    }

    private void StopInspector()
    {
        gameObject.SetActive(false);
    }

    public void SecurityGuardStatus()
    {
        StartCoroutine(MoveTourist(currentWP));

        AssignTimer();
    }

    void AssignTimer()
    {
        time = Vector3.Distance(transform.position, WalkPoints[0].transform.position) / movementSpeed;
        for (int i = 0; i < WalkPoints.Count; i++)
        {
            if (i + 1 != WalkPoints.Count)
            {
                time += Vector3.Distance(WalkPoints[i].transform.position, WalkPoints[i + 1].transform.position) / movementSpeed;
            }
        }
        time += timeofInspecting * WalkPoints.Count - 1;
        time -= timeofInspecting;
        time += 0.5f * WalkPoints.Count;
    }

    void Update()
    {
        TimerTextCountDown();
    }

    private IEnumerator MoveTourist(int wayPoint)
    {
        //Change animation from inspecting to walking.
        animator.SetBool(isInspecting, false);
        animator.SetBool(isWalking, true);

        //Look at (Change rotation) the current walkpoint.
        LookAtLookPoint(WalkPoints, currentWP);

        //Moves the Tourist along the walkpoint as long as the distance is bigger than the AccuracyWP.
        while (Vector3.Distance(transform.position, WalkPoints[wayPoint].transform.position) > accuracyWP)
        {
            transform.position = Vector3.MoveTowards(transform.position, WalkPoints[wayPoint].transform.position, movementSpeed * Time.deltaTime);
            yield return null;
        }

        //Look at (Change rotation) the current lookpoint.
        LookAtLookPoint(LookPoints, currentWP);

        //Start the inspecting timer.
        StartInspecting();

        //Change animation from walking to inspecting.
        InspectingState();

        //Start inspecting countdown.
        StartCoroutine(InspectingCountDown());
    }

    private void LookAtLookPoint(List<GameObject> lookAt, int index)
    {
        //Change rotation to look forward.
        Vector3 Target = lookAt[index].transform.position;
        Vector3 relativePos = Target - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    private void StartInspecting()
    {
        //Set time left to time of inspecting.
        timeLeft = timeofInspecting;
    }

    private void InspectingState()
    {
        animator.SetBool(isWalking, false);
        animator.SetBool(isInspecting, true);
    }

    private void CheckIfInspectingPlayer()
    {
        if (currentWP == WalkPoints.Count - 1)
        {
            playerZone.GetComponent<PlayerZone>().CheckIfThePlayerIsInSafeZone();
        }
    }

    private IEnumerator InspectingCountDown()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            CheckIfInspectingPlayer();
            yield return null;
        }

        ChangeWayPoint();

        StartCoroutine(MoveTourist(currentWP));
    }

    private void ChangeWayPoint()
    {
        //Change the waypoint of the tourist.
        currentWP++;
        //If the tourist reached his last walkpoint, resets walkpooints to 0 to restart his routine.
        if (currentWP >= WalkPoints.Count)
        {
            currentWP = 0;
            AssignTimer();
        }
    }

    public void TimerTextCountDown()
    {
        if (beginTimerTextCountDown == true)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;

            }

            if (TimerText)
            {
                TimerText.text = "00:" + time.ToString("0");
            }
        }
    }

    private IEnumerator CheckPlayerWhileInspecting()
    {
        yield return new WaitForSeconds(1f);
    }
}
