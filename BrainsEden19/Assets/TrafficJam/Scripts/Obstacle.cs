using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer ArrowSpriteRenderer;

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
}
