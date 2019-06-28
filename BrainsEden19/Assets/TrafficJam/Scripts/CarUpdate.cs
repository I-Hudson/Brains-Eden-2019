using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUpdate : MonoBehaviour
{
    public Transform[] SpawnLocations;
    public int spawnLocIndex;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
