﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    public Transform ball;
    public Transform aimTargetLeft; //left ref. to camera (player) position
    public Transform aimTargetRight;
    public Transform aimTargetCenter;


    private float speed;
    private float shotForce = 4.5f;
    private Vector3 targetPosition;
    private float shootingDir;
    //private float distanceFromBall;

    // Start is called before the first frame update
    void Start()
    {
        //don't render aimTargets
        aimTargetLeft.gameObject.GetComponent<Renderer>().enabled = false;
        aimTargetRight.gameObject.GetComponent<Renderer>().enabled = false;
        aimTargetCenter.gameObject.GetComponent<Renderer>().enabled = false;


        targetPosition = transform.position;
        SetBotSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        //distanceFromBall = (ball.position - gameObject.transform.position).magnitude;
        Move();
    }

    void Move()
    {
        targetPosition.x = ball.position.x;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            shootingDir = Random.Range(0.0f, 10.0f);
            if (shootingDir < 4.0f)
            {
                //shoot to the left of the player
                Debug.Log("Opponent hit the ball!");
                Vector3 dir = aimTargetLeft.position - transform.position;
                Debug.Log("Opponent targeting LEFT");
                other.GetComponent<Rigidbody>().velocity = dir.normalized * shotForce + new Vector3(0.0f, 3.2f, 0.0f);
            }
            else if (shootingDir >= 4.0f && shootingDir <= 6.0f)
            {
                //shoot to the center of the table
                Debug.Log("Opponent hit the ball!");
                Vector3 dir = aimTargetCenter.position - transform.position;
                Debug.Log("Opponent targeting CENTER");
                other.GetComponent<Rigidbody>().velocity = dir.normalized * shotForce + new Vector3(0.0f, 3.2f, 0.0f);
            }
            else
            {
                //shoot to the right of the player
                Debug.Log("Opponent hit the ball!");
                Vector3 dir = aimTargetRight.position - transform.position;
                Debug.Log("Opponent targeting RIGHT");
                other.GetComponent<Rigidbody>().velocity = dir.normalized * shotForce + new Vector3(0.0f, 3.2f, 0.0f);
            }
        }
    }

    void SetBotSpeed ()
    {
        int ctd = PlayerPrefs.GetInt("countdown");
        if (ctd == 25)
            speed = 1.25f;
        else if (ctd == 15)
            speed = 1.3f;
        else if (ctd == 10)
            speed = 1.35f;
        else
            Debug.LogWarning("Unknown Bot Speed");
    }
}

