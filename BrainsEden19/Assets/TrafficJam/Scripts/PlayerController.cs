using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

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

    public List<ObstacleSpawn> ObstacleSpawnGreenList = new List<ObstacleSpawn>();
    public List<ObstacleSpawn> ObstacleSpawnRedList = new List<ObstacleSpawn>();
    public List<ObstacleSpawn> ObstacleSpawnBlueList = new List<ObstacleSpawn>();
    public List<ObstacleSpawn> ObstacleSpawnYellowList = new List<ObstacleSpawn>();

    public Obstacles currentObstacle = Obstacles.Speed_Bumps;

    [SerializeField]
    private GameObject[] obstaclePrefabs;

    public ObstacleSpawn currentObstacleSpawn;

    public enum Obstacles
    {
        Speed_Bumps,
        RoadWorks,
        Old_Woman,
        Jam_Spill,

        END_OF_ENUM
    }

    PlayerIndex playerIndex;

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

        GameObject tl0 = trafficLightsList[0];
        GameObject tl1 = trafficLightsList[1];
        if(tl0.name == "TrafficLightPrefabLB")
        {
            trafficLightsList[0] = tl0;
            trafficLightsList[1] = tl1;
        }
        else
        {
            trafficLightsList[0] = tl1;
            trafficLightsList[1] = tl0;
        }
        


        GameObject[] listOfAllObstacleSpawn = GameObject.FindGameObjectsWithTag("ObstacleSpawnPoint");
        foreach(GameObject os in listOfAllObstacleSpawn)
        {
           
            switch(os.GetComponent<ObstacleSpawn>().iPlayerIndex)
            {
                case 0:
                    ObstacleSpawnGreenList.Add(os.GetComponent<ObstacleSpawn>());
                    break;
                case 1:
                    ObstacleSpawnRedList.Add(os.GetComponent<ObstacleSpawn>());
                    break;
                case 2:
                    ObstacleSpawnBlueList.Add(os.GetComponent<ObstacleSpawn>());
                    break;
                case 3:
                    ObstacleSpawnYellowList.Add(os.GetComponent<ObstacleSpawn>());
                    break;
                default:
                    break;
            }
            
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
            GamePad.SetVibration((PlayerIndex)iPlayerIndex+1, 1,0);
            JoystickBumperPressed(0);
            Debug.Log("joystick " + (iPlayerIndex + 1) + " button 4");
        }
        else if (Input.GetKeyDown("joystick "+ (iPlayerIndex + 1) + " button 5"))//right Bumper
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 0, 1);
            JoystickBumperPressed(1);
            Debug.Log("joystick " + (iPlayerIndex + 1) + " button 5");
        }

        if (Input.GetKeyDown("joystick " + (iPlayerIndex + 1) + " button 0"))// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            ColorButtonPressed(0);
            Debug.Log("A button Pressed");
        }
        else if (Input.GetKeyDown("joystick " + (iPlayerIndex + 1) + " button 1"))// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            ColorButtonPressed(1);
            Debug.Log("A button Pressed");
        }
        else if (Input.GetKeyDown("joystick " + (iPlayerIndex + 1) + " button 2"))// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            ColorButtonPressed(2);
            Debug.Log("A button Pressed");
        }
        else if (Input.GetKeyDown("joystick " + (iPlayerIndex + 1) + " button 3"))// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            ColorButtonPressed(3);
            Debug.Log("A button Pressed");
        }

        if (Input.GetAxis("DPadX" + (iPlayerIndex + 1)) >= 0.1)// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            Debug.Log("DPad Right");
            SwitchObstacleType(0);
        }
        else if (Input.GetAxis("DPadX" + (iPlayerIndex + 1)) <= -0.1)// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            Debug.Log("DPad Left");
            SwitchObstacleType(1);
        }
        else if (Input.GetAxis("DPadY" + (iPlayerIndex + 1)) >= 0.1)// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            Debug.Log("DPad Up");
            SwitchObstacleType(2);
        }
        else if (Input.GetAxis("DPadY" + (iPlayerIndex + 1)) <= -0.1)// X Button
        {
            GamePad.SetVibration((PlayerIndex)iPlayerIndex + 1, 1, 1);
            Debug.Log("DPad Down");
            SwitchObstacleType(3);
        }
    }

    private void FixedUpdate()
    {
        GamePad.SetVibration((PlayerIndex)iPlayerIndex, 0, 0);
    }

    //called when either left or right joystick bumper pressed, 0 = left, 1 = right
    private void JoystickBumperPressed(int a_iBumperIndex)
    {
        TrafficLight trafficLight = trafficLightsList[a_iBumperIndex].GetComponent<TrafficLight>();
        if (trafficLight)
        {
            trafficLight.SetActive();
            //GamePad.SetVibration(0, testA, testB);
        }
    }

    private void SwitchObstacleType(int obstacleIndex)
    {
        switch(obstacleIndex)
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
            case 4:
                currentObstacle = Obstacles.Jam_Spill;
                break;
            default:
                break;
        }
    }

    //called when the player pressed the A button
    private void ColorButtonPressed(int axybButtonIndex)
    {
        ObstacleSpawn selectedRoad;
        switch (axybButtonIndex)
        {
            case 0:
                selectedRoad = ObstacleSpawnBlueList[Random.Range(0, ObstacleSpawnGreenList.Count)];
                break;
            case 1:
                selectedRoad = ObstacleSpawnYellowList[Random.Range(0, ObstacleSpawnRedList.Count)];
                break;
            case 2:
                selectedRoad = ObstacleSpawnGreenList[Random.Range(0, ObstacleSpawnBlueList.Count)];
                break;
            case 3:
                selectedRoad = ObstacleSpawnRedList[Random.Range(0, ObstacleSpawnYellowList.Count)];
                break;
            default:
                selectedRoad = ObstacleSpawnGreenList[Random.Range(0, ObstacleSpawnGreenList.Count)];
                break;
        }
        
        if (selectedRoad)
        {
            selectedRoad.StartCoroutine(selectedRoad.SpawnObstacle(obstaclePrefabs[(int)currentObstacle], iPlayerIndex));
        }
    }
}
