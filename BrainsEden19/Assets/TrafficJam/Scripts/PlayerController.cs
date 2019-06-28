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
        TrafficLight[] listOfAllTrafficLights = GameObject.FindObjectsOfType<TrafficLight>();
        foreach (TrafficLight tl in listOfAllTrafficLights)
        {
            if (tl.iPlayerIndex == iPlayerIndex)
            {
                trafficLightsList.Add(tl.gameObject);
            }
        }

        ObstacleSpawn[] listOfAllObstacleSpawn = GameObject.FindObjectsOfType<ObstacleSpawn>();
        foreach(ObstacleSpawn os in listOfAllObstacleSpawn)
        {
            ObstacleRoadList.Add(os);
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
        switch(iPlayerIndex)
        {
            case 0:
                
                if (Input.GetKeyDown(KeyCode.Joystick1Button4))//Left Bumper
                {
                    JoystickBumperPressed(0);
                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button5))//right Bumper
                {
                    JoystickBumperPressed(1);
                }

                if (Input.GetKeyDown(KeyCode.Joystick1Button2))// X Button
                {
                    SwapInteractionMode();
                }
                break;
            default:
                break;
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
                iCurrentObstacleIndex = (int)(Obstacles.END_OF_ENUM) - 1;
            }
            else if (iCurrentObstacleIndex >= (int)(Obstacles.END_OF_ENUM))
            {
                iCurrentObstacleIndex = 0;
            }
        }

        ClearDeactiveatedHighlightedAreas();
    }

    private void ClearDeactiveatedHighlightedAreas()
    {
        //unHighlight all obstacles
        for (int i = 0; i < (int)(Obstacles.END_OF_ENUM) - 1; ++i)
        {
            ObstacleRoadList[i].HighLightArea(false);
        }
        //unHighlight all traffic lights
        for (int i = 0; i < trafficLightsList.Count; ++i)
        {
            trafficLightsList[i].GetComponent<TrafficLight>().HighLightArea(false);
        }

        if (interactionMode == InteractionMode.Mode_TrafficLight)
        {
            trafficLightsList[iCurrentTrafficLightIndex].GetComponent<TrafficLight>().HighLightArea(true);
        }
        else
        {
            ObstacleRoadList[iCurrentObstacleIndex].GetComponent<ObstacleSpawn>().HighLightArea(true);
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
        if(interactionMode == InteractionMode.Mode_TrafficLight)
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
                selectedRoad.SpawnObstacle(currentObstacle);
            }
        }
    }
}
