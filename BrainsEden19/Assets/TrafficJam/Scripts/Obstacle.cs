using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer ArrowSpriteRenderer;

    public int obstacleType = 0;

    public void SetColor(int playerIndex)
    {
        switch(playerIndex)
        {
            case 0:
                ArrowSpriteRenderer.color = Color.green;
                break;
            case 1:
                ArrowSpriteRenderer.color = Color.red;
                break;
            case 2:
                ArrowSpriteRenderer.color = Color.blue;
                break;
            case 3:
                ArrowSpriteRenderer.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        Transform lastTransform = ArrowSpriteRenderer.transform;
        lastTransform.LookAt(Camera.main.transform);
        Quaternion rot = lastTransform.rotation;
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        ArrowSpriteRenderer.transform.rotation = rot;
    }
}
