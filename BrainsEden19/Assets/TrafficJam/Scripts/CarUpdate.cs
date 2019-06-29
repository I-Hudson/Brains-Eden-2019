﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarColour
{
    Red, Green, Blue, Yellow
}

public class CarUpdate : MonoBehaviour
{
    public Transform[] SpawnLocations;
    public int spawnLocIndex;
    public float InitalSpeed;
    public float NormalSpeed;
    public float speed_;
    public float MinHitDistance;
    public bool isOnMainRoad = true;

    public CarColour CarColour;

    private bool update = true;

    [SerializeField]
    private bool forceForward = false;

    private bool isLooping = false;

    private bool isSlow = false;

    public bool TurnedIntoZone = false;

    // Update is called once per frame
    void Update()
    {
        if(!update)
        {
            return;
        }

        transform.position += transform.forward * speed_ * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);
        bool hitCar = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].distance <= MinHitDistance)
            {
                if (hits[i].collider.gameObject != gameObject && hits[i].collider.tag == "Car")
                {
                    speed_ = hits[i].collider.gameObject.GetComponent<CarUpdate>().speed_;
                    if (speed_ > NormalSpeed)
                    {
                        if (isOnMainRoad)
                        {
                            speed_ = NormalSpeed;
                        }
                    }
                }
                else
                {
                    if (hits[i].collider.gameObject != gameObject)
                    {
                        if (hits[i].collider.tag == "CenterTrafficLight")
                        {
                            if (!forceForward)
                            {
                                speed_ = 0;
                            }
                        }
                    }
                }
                hitCar = true;
                break;
            }
        }

        if (!hitCar && speed_ == 0)
        {
            speed_ = NormalSpeed;
        }
        else if(!hitCar)
        {
            speed_ = NormalSpeed;
        }
    }

    private void LateUpdate()
    {
        if(forceForward)
        {
            //speed_ = NormalSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ForceForward")
        {
            forceForward = false;
        }

        if (other.tag == "LoopTrigger")
        {
            isLooping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LoopTrigger")
        {
            if (!isLooping)
            {
                isLooping = true;
                if (spawnLocIndex == 0)
                {
                    spawnLocIndex = 1;
                }
                else if (spawnLocIndex == 1)
                {
                    spawnLocIndex = 0;
                }
                else if (spawnLocIndex == 2)
                {
                    spawnLocIndex = 3;
                }
                else if (spawnLocIndex == 3)
                {
                    spawnLocIndex = 2;
                }

                transform.position = SpawnLocations[spawnLocIndex].position;
                transform.rotation = SpawnLocations[spawnLocIndex].rotation;
            }
        }

        if (other.tag == "Car")
        {
            update = false;
            ScoreSystem.Instance.RemoveScore(CarColour, 1);
            StartCoroutine(CarCrashed());
        }

        if (other.tag == "ForceForward")
        {
            forceForward = true;
        }

        if(other.tag == "Obstacle_RoadWorks")
        {

        }
        if (other.tag == "Obstacle_SpeedBump")
        {
            isOnMainRoad = false;

            //slow down car speed by some amount for some time
            StartCoroutine(SlowDown());
        }
        if (other.tag == "Obstacle_WomanCrossing")
        {
            StartCoroutine(WomanCrossing());
        }
    }

    IEnumerator WomanCrossing()
    {
        speed_ = 0;

        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator SlowDown()
    {
        speed_ = 4;
        Debug.Log("Start Slow Down");
        yield return new WaitForSecondsRealtime(3.5f);
        Debug.Log("Stop Slow Down");
        speed_ = NormalSpeed;
    }

    IEnumerator CarCrashed()
    {
        yield return new WaitForSeconds(5.0f);

        FindObjectOfType<CarSpawner>().RemoveCar();
        Destroy(gameObject);
    }
}
