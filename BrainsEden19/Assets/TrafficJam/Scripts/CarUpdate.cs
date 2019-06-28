﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUpdate : MonoBehaviour
{
    public Transform[] SpawnLocations;
    public int spawnLocIndex;
    public float InitalSpeed;
    public float NormalSpeed;
    private float speed_;

    private bool forceForward = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed_ * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);
        bool hitCar = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject != gameObject && hits[i].collider.tag == "Car" ||
                hits[i].collider.gameObject != gameObject && hits[i].collider.tag == "CenterTrafficLight")
            {
                if (hits[i].distance < 2)
                {
                    speed_ = 0;
                    hitCar = true;
                    break;
                }

            }
        }

        if (!hitCar && speed_ == 0)
        {
            speed_ = NormalSpeed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "ForceForward")
        {
            forceForward = true;
        }
        else
        {
            forceForward = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LoopTrigger")
        {
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
}
