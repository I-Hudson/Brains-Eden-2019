﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    Car[] Cars;

    [SerializeField]
    [Tooltip("Number of cars to be spawned overall.")]
    private int numOfCarsSpawned;

    [SerializeField]
    Transform[] SpawnLocations;
    [SerializeField]
    int spawnLocIndex;

    Coroutine spawnCoro = null;

    [SerializeField]
    Color[] Colours;
    [SerializeField]
    CarColour[] CarColourKey;

    [SerializeField]
    private bool spawnCars = true;

    [SerializeField]
    private float SingleCarDelay;
    [SerializeField]
    private Vector2 MinMaxSpawnTime;

    [SerializeField]
    private float MinSpawDistance;

    [SerializeField]
    private int maxCarCount;
    [SerializeField]
    private int minCarCount;
    private int carCount = 0;

    private List<GameObject> line1Cars = new List<GameObject>();
    private List<GameObject> line2Cars = new List<GameObject>();

    [SerializeField]
    private Color[] initalColours;
    private bool isFirstSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.UtcNow.Ticks);
        spawnCoro = StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        if(carCount >= maxCarCount)
        {
            spawnCars = false;
        }
        else if(carCount < minCarCount)
        {
            spawnCars = true;
        }
    }

    public void RemoveCar()
    {
        carCount -= 1;
    }

    IEnumerator SpawnCars()
    {
        List<int> spawnLocationsUsed = new List<int> { 0, 1, 2, 3 };
        List<Color> spawnColourUsed = new List<Color>(Colours);
        int initalIndex = 0;

        while (spawnCars)
        {
            if(spawnLocationsUsed.Count == 0)
            {
                spawnLocationsUsed = new List<int> { 0, 1, 2, 3 };
            }
            if(spawnColourUsed.Count == 0)
            {
                spawnColourUsed = new List<Color>(Colours);
            }

            List<Color> line1Colours = new List<Color>();
            List<Color> line2Colours = new List<Color>();

            int randomSpawnLocation = Random.Range(0, spawnLocationsUsed.Count);
            randomSpawnLocation = spawnLocationsUsed[randomSpawnLocation];
            spawnLocationsUsed.Remove(randomSpawnLocation);

            if(!CheckIfSpawnIsPossible(SpawnLocations[randomSpawnLocation].position))
            {
                yield return new WaitForSeconds(Random.Range(3.0f, 3.5f));
            }

            for (int i = 0; i < numOfCarsSpawned; i++)
            {
                int randomCar = Random.Range(0, Cars.Length);

                if (isFirstSpawn && initalIndex < 4)
                {
                    SpawnCar(randomCar, randomSpawnLocation, initalColours[i]);
                }

                int randomColour = Random.Range(0, spawnColourUsed.Count);//Random.Range(0, Colours.Length - 1);
                Color rColour = spawnColourUsed[randomColour];//Colours[randomColour];
                spawnColourUsed.Remove(rColour);
                //rColour = CheckColour(line1Colours, rColour);

                line1Colours.Add(rColour);

                CarColourCheck(randomSpawnLocation, rColour);

                SpawnCar(randomCar, randomSpawnLocation, rColour);

                yield return new WaitForSeconds(SingleCarDelay);
            }
            carCount += numOfCarsSpawned;
            initalIndex++;
            yield return new WaitForSeconds(Random.Range(MinMaxSpawnTime.x, MinMaxSpawnTime.y));
        }
    }

    private bool CheckIfSpawnIsPossible(Vector3 aSpawnLoc)
    {
        CarUpdate[] allCars = FindObjectsOfType<CarUpdate>();

        for (int i = 0; i < allCars.Length; i++)
        {
            if(Vector3.Distance(allCars[i].transform.position, aSpawnLoc) < MinSpawDistance)
            {
                return false;
            }
        }

        return true;
    }

    private Color CheckColour(List<Color> aList, Color aColour)
    {
        if(aList.Count == 0)
        {
            return aColour;
        }

        int i = 0;
        Color c = aColour;
        while(aList[aList.Count - 1] == c)
        {
            c = Colours[i];
            i = (i + 1) % Colours.Length;
        }

        return c;
    }

    private Color CarColourCheck(int aSpawnLoc, Color aColour)
    {
        //check with a raycast the colour of the car in front. if it's the same colour change this 
        //colour
        return aColour;
    }

    private void SpawnCar(int aRandomCar, int aSpawnLocIndex, Color aColor)
    {
        GameObject go = Instantiate(Cars[aRandomCar].Model, new Vector3(SpawnLocations[aSpawnLocIndex].position.x,
                                                                        Cars[aRandomCar].Model.transform.position.y, 
                                                                        SpawnLocations[aSpawnLocIndex].position.z),
                    SpawnLocations[aSpawnLocIndex].rotation, transform);
        go.name = Cars[aRandomCar].name;

        if (!go.GetComponent<CarUpdate>())
        {
            go.AddComponent<CarUpdate>();
        }
        go.GetComponent<CarUpdate>().InitalSpeed = Cars[aRandomCar].MaxSpeed;
        go.GetComponent<CarUpdate>().NormalSpeed = Cars[aRandomCar].ControlSpeed;
        go.GetComponent<CarUpdate>().spawnLocIndex = aSpawnLocIndex;
        go.GetComponent<CarUpdate>().SpawnLocations = SpawnLocations;
        go.GetComponent<CarUpdate>().MinHitDistance = Cars[aRandomCar].MinHitDistance;
        go.GetComponent<CarUpdate>().CarType = Cars[aRandomCar].CarType;


        for (int i = 0; i < CarColourKey.Length; i++)
        {
            if(Colours[i] == aColor)
            {
                go.GetComponent<CarUpdate>().CarColour = CarColourKey[i];
                break;
            }
        }
        go.tag = Cars[aRandomCar].Tag;
        go.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("Color_891AA065", aColor);

        if (aSpawnLocIndex == 0 || aSpawnLocIndex == 1)
        {
            line1Cars.Add(go);
        }
        else if (aSpawnLocIndex == 2 || aSpawnLocIndex == 3)
        {
            line2Cars.Add(go);
        }
    }
}
