using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarColour
{
    Red, Green, Blue, Yellow
}

public enum CarType
{
    Hot_Hatch, Muscle, Truck, Van
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

    [SerializeField]
    private Transform rayCastStartPos;

    public CarColour CarColour;
    public CarType CarType;

    private bool update = true;

    [SerializeField]
    private bool forceForward = false;

    [SerializeField]
    private bool forceStop = false;

    private bool isLooping = false;

    private bool isSlow = false;

    public bool TurnedIntoZone = false;

    private void Start()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if(transform.GetChild(i).name == "rayCastStartPos")
            {
                rayCastStartPos = transform.GetChild(i);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!update)
        {
            return;
        }

        transform.position += transform.forward * speed_ * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(rayCastStartPos.position, transform.forward);
        bool hitCar = false;
        bool hitTrafficLights = false;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].distance <= MinHitDistance)
            {
                if (hits[i].collider.gameObject != gameObject && hits[i].collider.tag == "Car")
                {
                    Debug.DrawRay(rayCastStartPos.position, transform.forward * hits[i].distance, Color.magenta, 5.0f);
                    speed_ = hits[i].collider.gameObject.GetComponent<CarUpdate>().speed_;

                    if (speed_ > NormalSpeed)
                    {
                        if (isOnMainRoad)
                        {
                            speed_ = NormalSpeed;
                        }
                    }
                    hitCar = true;
                }
                else
                {
                    if (hits[i].collider.gameObject != gameObject)
                    {
                        if (hits[i].collider.tag == "CenterTrafficLight")
                        {
                            if (hits[i].distance <= 5.0f)
                            {
                                if (!forceForward)
                                {
                                    speed_ = 0;
                                    hitCar = true;
                                    break;
                                }
                            }
                        }
                    }
                }
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

    private void LoopVehicle()
    {
        transform.position = new Vector3(SpawnLocations[spawnLocIndex].position.x, transform.position.y, SpawnLocations[spawnLocIndex].position.z);
        transform.rotation = SpawnLocations[spawnLocIndex].rotation;
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

                //LoopVehicle();
            }
        }

        if (other.tag == "Car")
        {
            if (update)
            {
                update = false;
                ScoreSystem.Instance.Crash(transform.position);
                FindObjectOfType<CarSpawner>().RemoveCar(this);
                Destroy(gameObject, 5.0f);
            }
        }

        if(other.tag == "CenterTrafficLight")
        {
            if (!forceForward)
            {
                //speed_ = 0;
            }
            forceStop = true;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CenterTrafficLight")
        {
            forceStop = false;
        }
    }

    IEnumerator WomanCrossing()
    {
        speed_ = 0;

        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator SlowDown()
    {
        speed_ *= 0.5f;
        Debug.Log("Start Slow Down");
        yield return new WaitForSecondsRealtime(3.5f);
        Debug.Log("Stop Slow Down");
        speed_ = NormalSpeed;
    }

    private CarColour SectionToCarColour(string aSectionString)
    {
        switch(aSectionString)
        {
            case "YellowSectionBox":
                return CarColour.Yellow;

            case "BlueSectionBox":
                return CarColour.Blue;

            case "GreenSectionBox":
                return CarColour.Green;

            case "RedSectionBox":
                return CarColour.Red;
        }

        return CarColour.Blue;
    }

    IEnumerator CarCrashed()
    {

        yield return null;
        //List<GameObject> sectionTriggerBoxs = new List<GameObject>();
        //sectionTriggerBoxs.Add(GameObject.FindGameObjectWithTag("YellowSectionBox"));
        //sectionTriggerBoxs.Add(GameObject.FindGameObjectWithTag("BlueSectionBox"));
        //sectionTriggerBoxs.Add(GameObject.FindGameObjectWithTag("GreenSectionBox"));
        //sectionTriggerBoxs.Add(GameObject.FindGameObjectWithTag("RedSectionBox"));

        //for (int i = 0; i < sectionTriggerBoxs.Count; i++)
        //{
        //    if (sectionTriggerBoxs[i].GetComponent<Collider>().bounds.Contains(transform.position))
        //    {
        //        ScoreSystem.Instance.RemoveScore(SectionToCarColour(sectionTriggerBoxs[i].tag), 1);
        //        
        //        break;
        //    }
        //}

        //yield return new WaitForSeconds(5.0f);
    }
}
