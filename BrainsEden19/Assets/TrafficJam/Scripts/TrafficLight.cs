using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public int iPlayerIndex = 0;

    [SerializeField]
    private float fMaxActiveTime = 3.0f;

    public bool bActive = false;

    public PlayerManager playerManager;
    private PlayerController myPlayer;

    [SerializeField]
    private GameObject highlightedArea;

    [SerializeField]
    private Material clearMat;

    [SerializeField]
    private Material whiteMat;

    [SerializeField]
    private Material trafflightGreen;
    [SerializeField]
    private Material trafflightRed;

    [SerializeField]
    private ObstacleSpawn myRoadObstcle;

    [SerializeField]
    private Light greenLight;
    [SerializeField]
    private Light amberLight;
    [SerializeField]
    private Light redLight;

    [SerializeField]
    private SpriteRenderer junctionArrow;

    [SerializeField]
    private Sprite StreightArrow;
    [SerializeField]
    private Sprite LeftArrow;

    [SerializeField]
    private GameObject selectionArrow;

    private void Start()
    {
        greenLight.enabled = false;
        amberLight.enabled = false;
        redLight.enabled = true;

        junctionArrow.sprite = StreightArrow;
        selectionArrow.SetActive(false);
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

            selectionArrow.SetActive(true);
        }
        else
        {
            selectionArrow.SetActive(false);
            highlightedArea.GetComponent<MeshRenderer>().material = clearMat;
        }
    }

    private bool CheckRoadClear()
    {
        if(myRoadObstcle && myRoadObstcle.currentObstacle && myRoadObstcle.currentObstacle.tag == "Obstacle_RoadWorks")
        {
            return false;
        }
        return true;
    }


    public void SetActive()
    {
        if (!bActive)
        {
            bActive = true;
            greenLight.enabled = true;
            redLight.enabled = false;
            junctionArrow.sprite = LeftArrow;
        }
        else if(bActive)
        {
            bActive = false;
            junctionArrow.sprite = StreightArrow;
            greenLight.enabled = false;
            redLight.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car" && bActive && CheckRoadClear() && !other.GetComponent<CarUpdate>().TurnedIntoZone)
        {
            other.transform.eulerAngles += new Vector3(0, -90, 0);
            other.GetComponent<CarUpdate>().TurnedIntoZone = true;
        }
    }
}
