using System.Collections;
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

    public CarColour CarColour;

    private bool update = true;

    [SerializeField]
    private bool forceForward = false;

    private bool isLooping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            if (hits[i].distance < MinHitDistance)
            {
                if (gameObject && hits[i].collider.tag == "Car")
                {
                    speed_ = hits[i].collider.gameObject.GetComponent<CarUpdate>().speed_;
                    if (speed_ > NormalSpeed)
                    {
                        speed_ = NormalSpeed;
                    }
                }
                else
                {
                    if (hits[i].collider.gameObject != gameObject && hits[i].collider.tag == "CenterTrafficLight" ||
                        hits[i].collider.gameObject != gameObject && hits[i].collider.tag == "Obstacle_RoadWorks")
                    {
                        if (!forceForward)
                        {
                            speed_ = 0;
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
        if(other.tag == "Obstacle_SpeedBump")
        {

        }
        if (other.tag == "Obstacle_WomanCrossing")
        {

        }
    }

    IEnumerator CarCrashed()
    {
        yield return new WaitForSeconds(5.0f);

        FindObjectOfType<CarSpawner>().RemoveCar();
        Destroy(gameObject);
    }
}
