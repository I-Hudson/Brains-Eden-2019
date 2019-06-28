using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUpdate : MonoBehaviour
{
    public Transform[] SpawnLocations;
    public int spawnLocIndex;
    public float InitalSpeed;
    public float NormalSpeed;
    private float speed_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed_ * Time.deltaTime;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.position + (transform.forward * 1.1f), out hit))
        {
            if(tag == "CenterTrafficLight")
            {
                Debug.Log("");
            }
            if (hit.collider.gameObject != gameObject && tag == "Car" ||
                hit.collider.gameObject != gameObject && tag == "CenterTrafficLight")
            {
                if (hit.distance < 2)
                {
                    speed_ = 0;
                }
            }
            if (speed_ == 0)
            {
                speed_ = NormalSpeed;
            }
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
