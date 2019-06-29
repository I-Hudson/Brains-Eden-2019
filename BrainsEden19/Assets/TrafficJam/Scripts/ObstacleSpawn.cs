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

    [SerializeField]
    private Material clearMat;

    [SerializeField]
    private Material whiteMat;

    public bool highlighted = false;

    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }
   

    public void HighLightArea(bool highlighted, int iPlayerIndex)
    {
        if (highlighted)
        {
            Material newMat = new Material(Shader.Find("HDRP/Lit"));
            switch (iPlayerIndex)
            {
                case 0:
                    newMat.SetColor("_BaseColor", Color.green);
                    break;
                case 1:
                    newMat.SetColor("_BaseColor", Color.red);
                    break;
                case 2:
                    newMat.SetColor("_BaseColor", Color.blue);
                    break;
                case 3:
                    newMat.SetColor("_BaseColor", Color.yellow);
                    break;
                default:
                    break;
            }
            highlightedArea.GetComponent<MeshRenderer>().material = newMat;
        }
        else
        {
            highlightedArea.GetComponent<MeshRenderer>().material = clearMat;
        }
    }

    public IEnumerator SpawnObstacle(GameObject obstacle)
    {
        if(currentObstacle)
        {
            RemoveObstacle();
        }
        currentObstacle = Instantiate(obstacle, transform.position, transform.rotation);

        yield return new WaitForSeconds(iTimeTillRemoved);

        RemoveObstacle();
    }

    private void RemoveObstacle()
    {
        Destroy(currentObstacle);
    }
}
