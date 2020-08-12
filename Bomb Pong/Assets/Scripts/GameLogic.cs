﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Player
{
    Human, // =0
    Ai // =1
}

public class GameLogic : MonoBehaviour
{
    public float countdown;
    public Text playerScoreTxt;
    public Text opponentScoreTxt;
    public Text countdownTxt;

    public GameObject canvas;

    public GameObject bomb;

    private Scene initScene;


    private Transform ballInitTransf;
    private Transform bombInitTransf;
    private static int playerScore = 0;
    private static int opponentScore = 0;
    private Player winsPoint;
    private Player lastTouchedBall;
    private int tableTouches;



    // Start is called before the first frame update
    void Start()
    {
        initScene = SceneManager.GetActiveScene();
        tableTouches = 0;
        countdown = 10;
        countdownTxt.text = countdown.ToString();


        ballInitTransf = transform;
        bombInitTransf = bomb.transform;

        lastTouchedBall = Player.Human;

    }

    // Update is called once per frame
    void Update()
    {
        playerScoreTxt.text = playerScore.ToString();
        opponentScoreTxt.text = opponentScore.ToString();

        countdown -= Time.deltaTime;
        countdownTxt.text = (Mathf.FloorToInt(countdown)).ToString();
        if (countdown<0.0f)
        {
            Debug.Log("BOOM!");
            winsPoint = Player.Human;
            AssignPoint(winsPoint);

            NewRally();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Out") || other.CompareTag("Wall"))
        {
            if (tableTouches == 0)
            {
                if (lastTouchedBall == Player.Ai)
                {
                    Debug.Log("AI OUT! Point for Human");
                    winsPoint = Player.Human;
                }
                else
                {
                    Debug.Log("Human OUT! Point for AI");
                    winsPoint = Player.Ai;
                }

                AssignPoint(winsPoint);

                NewRally();
            }
            if (tableTouches >= 1)
            {
                if (lastTouchedBall == Player.Ai)
                {
                    Debug.Log("Player missed! Point for AI");
                }
                else
                {
                    Debug.Log("AI missed! Point for Player");
                }
                winsPoint = lastTouchedBall;
                AssignPoint(winsPoint);

                NewRally();
            }
            
        }

        if (other.CompareTag("Player"))
        {
            lastTouchedBall = Player.Human;
            tableTouches = 0;
            return;
        }

        if (other.CompareTag("Opponent"))
        {
            lastTouchedBall = Player.Ai;
            tableTouches = 0;
            return;
        }

        if (other.CompareTag("Table"))
        {
            tableTouches++;
            if (tableTouches >= 2)
            {
                winsPoint = lastTouchedBall;
                AssignPoint(winsPoint);
                NewRally();
            }
        }
    }

    void AssignPoint (Player p)
    {
        if ((int)p == (int)Player.Human)
        {
            playerScore++;
        }
        else if ((int)p == (int)Player.Ai)
        {
            opponentScore++;
        }
    }

    public void NewRally() //reset to initial positions
    {
        Debug.Log("Starting New Rally!");
        tableTouches = 0;
        countdown = 10;

        SceneManager.LoadScene(initScene.name);
        
    }
}
