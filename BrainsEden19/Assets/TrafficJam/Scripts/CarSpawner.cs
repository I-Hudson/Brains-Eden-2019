using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    Car[] Cars;

    [SerializeField]
    [Tooltip("Number of cars to be spawned overall.")]
    int NumOfCarsSpawned;

    [SerializeField]
    Transform[] SpawnLocations;
    int spawnLocIndex;

    Coroutine spawnCoro = null;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocIndex = 0;
        spawnCoro = StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCars()
    {
        for (int i = 0; i < NumOfCarsSpawned; i++)
        {
            int randomCar = Random.Range(0, Cars.Length);

            //spawn car at locations
            for (int j = 0; j < 2; j++)
            {
                GameObject go = Instantiate(Cars[randomCar].Model, SpawnLocations[spawnLocIndex + j].position,
                                    SpawnLocations[spawnLocIndex + j].rotation, transform);
                if (!go.GetComponent<CarUpdate>())
                {
                    go.AddComponent<CarUpdate>();
                }
                go.GetComponent<CarUpdate>().speed = Cars[randomCar].Speed;
                go.GetComponent<CarUpdate>().spawnLocIndex = spawnLocIndex + j;
                go.GetComponent<CarUpdate>().SpawnLocations = SpawnLocations;
            }
        }
        yield return null;
    }
}
