using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Space(5)]
    [Header("General Settings")]
    public int iPlayerIndex = 0;

    [SerializeField]
    private InteractionMode interactionMode = InteractionMode.Mode_TrafficLight;

    private enum InteractionMode
    {
        Mode_TrafficLight,
        Mode_Obstacles,

        END_OF_ENUM
    }

    [Space(5)]
    [Header("Traffic Light Settings")]
     
    [SerializeField]
    private List<GameObject> trafficLightsList = new List<GameObject>();

    [SerializeField]
    private int iCurrentTrafficLightIndex = 0;

    [Space(5)]
    [Header("Obstacle Settings")]
    [SerializeField]
    private int iCurrentObstacleIndex = 0;

    [SerializeField]
    private List<ObstacleSpawn> ObstacleRoadList = new List<ObstacleSpawn>();

    public Obstacles currentObstacle = Obstacles.Speed_Bumps;

    [SerializeField]
    private GameObject[] obstaclePrefabs;

    [SerializeField]
    private int iObstaclePrefabIndex = 0;

    public ObstacleSpawn currentObstacleSpawn;

    public enum Obstacles
    {
        Speed_Bumps,
        RoadWorks,
        Old_Woman,

        END_OF_ENUM
    }

    // Use this for initialization
    public void UpdateTrafficLightList()
    {
        GameObject[] listOfAllTrafficLights = GameObject.FindGameObjectsWithTag("TrafficLightControlable");
        foreach (GameObject tl in listOfAllTrafficLights)
        {
            if (tl.GetComponent<TrafficLight>().iPlayerIndex == iPlayerIndex)
            {
                trafficLightsList.Add(tl);
            }
        }

        GameObject[] listOfAllObstacleSpawn = GameObject.FindGameObjectsWithTag("ObstacleSpawnPoint");
        foreach(GameObject os in listOfAllObstacleSpawn)
        {
            ObstacleRoadList.Add(os.GetComponent<ObstacleSpawn>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
    }

    //Main switch checking each joystick input
    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown("joystick " + (iPlayerIndex+1) + " button 4"))//Left Bumper
        {
            JoystickBumperPressed(0);
            Debug.Log("joystick " + (iPlayerIndex + 1) + " button 4");
        }
        else if (Input.GetKeyDown("joystick "+ (iPlayerIndex + 1) + " button 5"))//right Bumper
        {
            JoystickBumperPressed(1);
            Debug.Log("joystick " + (iPlayerIndex + 1) + " button 5");
        }

        if (Input.GetKeyDown("joystick " + (iPlayerIndex + 1) + " button 2"))// X Button
        {
            SwapInteractionMode();
            Debug.Log("joystick " + (iPlayerIndex + 1) + " button 2");
        }
        else if (Input.GetKeyDown("joystick " + (iPlayerIndex + 1) + " button 0"))// X Button
        {
            InteractionButtonPressed();
            Debug.Log("joystick " + (iPlayerIndex + 1) + " button 0");

        }
    }

    //called when either left or right joystick bumper pressed, 0 = left, 1 = right
    private void JoystickBumperPressed(int a_iBumperIndex)
    {
        if (interactionMode == InteractionMode.Mode_TrafficLight)
        {
            if (a_iBumperIndex == 0)//LB Pressed
            {
                iCurrentTrafficLightIndex--;
            }
            else if (a_iBumperIndex == 1)//RB Pressed
            {
                iCurrentTrafficLightIndex++;
            }

            if (iCurrentTrafficLightIndex < 0)
            {
                iCurrentTrafficLightIndex = trafficLightsList.Count - 1;
            }
            else if (iCurrentTrafficLightIndex >= trafficLightsList.Count)
            {
                iCurrentTrafficLightIndex = 0;
            }
        }
        else if (interactionMode == InteractionMode.Mode_Obstacles)
        {
            if (a_iBumperIndex == 0)//LB Pressed
            {
                iCurrentObstacleIndex--;
            }
            else if (a_iBumperIndex == 1)//RB Pressed
            {
                iCurrentObstacleIndex++;
            }

            if (iCurrentObstacleIndex < 0)
            {
                iCurrentObstacleIndex = ObstacleRoadList.Count - 1;
            }
            else if (iCurrentObstacleIndex > ObstacleRoadList.Count - 1)
            {
                iCurrentObstacleIndex = 0;
            }
        }

        ClearDeactiveatedHighlightedAreas();
    }

    private void ClearDeactiveatedHighlightedAreas()
    {
        //unHighlight all obstacles
        for (int i = 0; i < ObstacleRoadList.Count; ++i)
        {
            if (i == iCurrentObstacleIndex && interactionMode == InteractionMode.Mode_Obstacles)
            {
                ObstacleRoadList[i].HighLightArea(true, iPlayerIndex);
                ObstacleRoadList[i].highlighted = true;
                currentObstacleSpawn = ObstacleRoadList[i];
                //break;
            }
            else if(i != iCurrentObstacleIndex || interactionMode != InteractionMode.Mode_Obstacles)
            {
                ObstacleRoadList[i].highlighted = false;
            }
        }
        //unHighlight all traffic lights
        for (int i = 0; i < trafficLightsList.Count; ++i)
        {
            if (i != iCurrentTrafficLightIndex || interactionMode != InteractionMode.Mode_TrafficLight)
            {
                trafficLightsList[i].GetComponent<TrafficLight>().HighLightArea(false,iPlayerIndex);
            }
            else
            {
                trafficLightsList[i].GetComponent<TrafficLight>().HighLightArea(true, iPlayerIndex);
            }
        }
    }

    private void SwitchObstacleType()
    {
        switch(iCurrentObstacleIndex)
        {
            case 0:
                currentObstacle = Obstacles.Speed_Bumps;
                break;
            case 2:
                currentObstacle = Obstacles.RoadWorks;
                break;
            case 3:
                currentObstacle = Obstacles.Old_Woman;
                break;
            default:
                break;
        }
    }

    //Changes wether we are swicthing traffic lights or placing obstacles
    private void SwapInteractionMode()
    {
        ClearDeactiveatedHighlightedAreas();

        if (interactionMode == InteractionMode.Mode_TrafficLight)
        {
            interactionMode = InteractionMode.Mode_Obstacles;
        }
        else
        {
            interactionMode = InteractionMode.Mode_TrafficLight;
        }
    }

    //called when the player pressed the A button
    private void InteractionButtonPressed()
    {
        if (interactionMode == InteractionMode.Mode_TrafficLight)
        {
            TrafficLight trafficLight = trafficLightsList[iCurrentTrafficLightIndex].GetComponent<TrafficLight>();
            if (trafficLight)
            {
                trafficLight.StartCoroutine(trafficLight.SetActive());
            }
        }
        else if(interactionMode == InteractionMode.Mode_Obstacles)
        {
            ObstacleSpawn selectedRoad = ObstacleRoadList[iCurrentObstacleIndex];
            if (selectedRoad)
            {
                selectedRoad.StartCoroutine(selectedRoad.SpawnObstacle(obstaclePrefabs[(int)currentObstacle]));
            }
        }
    }
}
