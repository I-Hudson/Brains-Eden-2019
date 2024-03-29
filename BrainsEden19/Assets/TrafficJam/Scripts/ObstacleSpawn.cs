﻿using UnityEngine;
using System.Collections;

public class ObstacleSpawn : MonoBehaviour
{
    public int iPlayerIndex = 0;

    private float iTimeTillRemoved = 8.0f;

    public PlayerManager playerManager;
    private PlayerController myPlayer;

    public GameObject currentObstacle;

    [SerializeField]
    private GameObject highlightedArea;

    [SerializeField]
    private Material clearMat;

    [SerializeField]
    private Material whiteMat;

    public bool highlighted = false;


    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        for (int i = 0; i < playerManager.players.Count; ++i)
        {
            if(playerManager.players[i].iPlayerIndex == iPlayerIndex)
            myPlayer = playerManager.players[i];
        }
    }
   
    public IEnumerator SpawnObstacle(GameObject obstacle, int playerIndex)
    {
        if(currentObstacle)
        {
            RemoveObstacle();
        }
        currentObstacle = Instantiate(obstacle, transform.position, transform.rotation);
        currentObstacle.GetComponent<Obstacle>().SetColor(playerIndex);

        yield return new WaitForSeconds(iTimeTillRemoved);

        RemoveObstacle();
    }

    private void RemoveObstacle()
    {
        Destroy(currentObstacle);
    }
}
