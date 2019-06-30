using UnityEngine;
using System.Collections;

public class ObstaclePreviewSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] ObstaclePreviewPos;

    private GameObject[] currentObstacle = new GameObject[4];
    public void SpawnObstacle(GameObject obstacle, int playerIndex)
    {
        if (currentObstacle[playerIndex])
        {
            Destroy(currentObstacle[playerIndex]);
        }

        foreach (SpriteRenderer ob in obstacle.GetComponentsInChildren<SpriteRenderer>())
        {
            ob.enabled = false;
        }
        
        currentObstacle[playerIndex] = Instantiate(obstacle, ObstaclePreviewPos[playerIndex].position, transform.rotation);
    }
}
