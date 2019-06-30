using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObstaclePreviewSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] ObstaclePreviewPos;

    private GameObject[] currentObstacle = new GameObject[4];

    public Coroutine[] spawnCoros = new Coroutine[4];

    [SerializeField]
    Image[] previewImages;

    [SerializeField]
    float timerCooldown;

    public void SpawnObstacle(GameObject obstacle, int playerIndex)
    {
        if (currentObstacle[playerIndex])
        {
            Destroy(currentObstacle[playerIndex]);
        }

        if (spawnCoros[playerIndex] == null)
        {
            spawnCoros[playerIndex] = StartCoroutine(Timer(obstacle, playerIndex));
        }

        
    }

    IEnumerator Timer(GameObject obstacle, int playerIndex)
    {
        float step = 0.0f;
        while(step < timerCooldown)
        {
            step += Time.deltaTime;
            previewImages[playerIndex].fillAmount = step / timerCooldown;
            yield return null;
        }

        currentObstacle[playerIndex] = Instantiate(obstacle, ObstaclePreviewPos[playerIndex].position, transform.rotation);
        spawnCoros[playerIndex] = null;

        foreach (SpriteRenderer sr in currentObstacle[playerIndex].GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }
    }
}
