using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;
    private GameObject Player;
    [HideInInspector] public bool isPlayerCaught = false;
    public SecurityGuardManager securityGuardManager;
    public GameObject playerZone;
    public CarManager carManager;
    public LevelEnd levelEnd;
    public event Action LevelFinished;
    private string endScreenLosingText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Search for the Player with Player tag.
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Hatem: Can't find the player, Check if you put tag 'Player' on the Player");
        }

        playerZone = GameObject.FindGameObjectWithTag("PlayerStartPosition");
        if (playerZone == null)
        {
            Debug.LogError("Hatem: Can't find the playerZone, Check if you put tag 'playerStartPosition' on the PlayerZone");
        }
        else
        {
            Player.transform.position = playerZone.transform.position;
        }

        carManager.PlayerFinished += EndGame;
    }

    //TODO: Remove Later.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }

    public void CatchPlayer(string losingText)
    {
        Debug.Log(losingText);
        if (!isPlayerCaught)
        {
            Vibration.Vibrate(50);
            AudioManager.instance.MuteBackgroundMusic(true);
            AudioManager.instance.PlaySoundEffect(0);
            Player.GetComponent<Player>().Lost();
            for (int i = 0; i < securityGuardManager.securityGuards.Count; i++)
            {
                securityGuardManager.securityGuards[i].GetComponent<SecurityGuard>().PersuitPlayer();
            }
            isPlayerCaught = true;
            endScreenLosingText = losingText;
        }
    }

    public void SecurityGuardReachedPlayer()
    {
        levelEnd.FillLoseScreenData(endScreenLosingText);
        levelEnd.GetComponent<LevelEndAnimationManager>().StartLosingAnimation();
        LevelFinished?.Invoke();

        for (int i = 0; i < securityGuardManager.securityGuards.Count; i++)
        {
            foreach (Transform child in securityGuardManager.securityGuards[i].transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        Player.SetActive(false);
    }

    public bool PlayerMovingStatus()
    {
        return Player.GetComponent<PlayerMovementAndRotation>().PlayerMovementStatus();
    }

    public void EndGame()
    {
        bool isLevelCompleted = ScoreManager.instance.CanCompleteLevel();
        string levelNumber = PlayerPrefs.GetInt("Level").ToString();
        int numberOfStars = ScoreManager.instance.CalculateStars();

        ScoreManager.instance.CalculateStars();

        if (isLevelCompleted)
        {
            levelEnd.FillWinScreenData(levelNumber, numberOfStars);
            levelEnd.GetComponent<LevelEndAnimationManager>().StartWinningAnimation();
        }
        else
        {
            AudioManager.instance.MuteBackgroundMusic(true);
            AudioManager.instance.PlaySoundEffect(0);
            levelEnd.FillLoseScreenData("Not enough loot!");
            levelEnd.GetComponent<LevelEndAnimationManager>().StartLosingAnimation();
        }

        Player.SetActive(false);

        LevelFinished?.Invoke();
    }
}
