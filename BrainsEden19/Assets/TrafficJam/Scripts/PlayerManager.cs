using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private int iPlayerCount = 4;

    [SerializeField]
    private GameObject playerPrefab;

    [Header("Debug Settings")]
    [SerializeField]
    private bool overideNeededPlayers = false;

    [SerializeField]
    private TextMeshProUGUI debugTextMesh;

    public List<PlayerController> players = new List<PlayerController>();

    [SerializeField]
    private List<ObstacleSpawn> ObstacleRoadList = new List<ObstacleSpawn>();

    private void Start()
    {
        for (int i = 0; i < iPlayerCount; ++i)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();
            newPlayerController.iPlayerIndex = i;
            newPlayerController.UpdateTrafficLightList();

            players.Add(newPlayerController);
        }

        GameObject[] listOfAllObstacleSpawn = GameObject.FindGameObjectsWithTag("ObstacleSpawnPoint");
        foreach (GameObject os in listOfAllObstacleSpawn)
        {
            ObstacleRoadList.Add(os.GetComponent<ObstacleSpawn>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPlayersConnected() || overideNeededPlayers)
        {
            Time.timeScale = 1;
            //debugTextMesh.text = "";
        }
        else
        {
            debugTextMesh.text = "4 Players Need to be Connected";
            Time.timeScale = 0;
        }


    }

    //If 4 players connected, returns true
    private bool CheckPlayersConnected()
    {
        if(Input.GetJoystickNames().Length >= 4)
        {
            return true;
        }
        return false;
    }

    
}
