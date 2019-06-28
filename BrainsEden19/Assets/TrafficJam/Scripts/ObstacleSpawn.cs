using UnityEngine;
using System.Collections;

public class ObstacleSpawn : MonoBehaviour
{
    public int iPlayerIndex = 0;

    [SerializeField]
    private float iTimeTillRemoved = 5.0f;

    public PlayerManager playerManager;

    private GameObject currentObstacle;

    [SerializeField]
    private GameObject highlightedArea;

    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void HighLightArea(bool highlighted)
    {
        if(highlighted)
        {
            highlightedArea.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 1.0f,0.4f);
        }
        else
        {
            highlightedArea.GetComponent<MeshRenderer>().material.color = Color.clear;
        }
    }

    public IEnumerator SpawnObstacle(PlayerController.Obstacles obstacle)
    {
        if(currentObstacle)
        {
            RemoveObstacle();
        }
        currentObstacle = Instantiate(playerManager.obstaclePrefabs[(int)obstacle], transform.position, transform.rotation);

        yield return new WaitForSeconds(iTimeTillRemoved);

        RemoveObstacle();
    }

    private void RemoveObstacle()
    {
        Destroy(currentObstacle);
    }
}
