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

    private void Start()
    {
        greenLight.enabled = false;
        amberLight.enabled = false;
        redLight.enabled = true;
    }
    public void HighLightArea(bool highlighted, int iPlayerIndex)
    {
        if (highlighted)
        {
            switch(iPlayerIndex)
            {
                case 0:
                    whiteMat.color = new Color(0,1,0,1);
                    break;
                case 1:
                    whiteMat.color = new Color(0, 1, 0, 1);
                    break;
                case 2:
                    whiteMat.color = new Color(0, 1, 0, 1);
                    break;
                case 3:
                    whiteMat.color = new Color(0, 1, 0, 1);
                    break;
                default:
                    break;
            }
            
            highlightedArea.GetComponent<MeshRenderer>().material = whiteMat;
        }
        else
        {
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


    public IEnumerator SetActive()
    {
        if (!bActive)
        {
            bActive = true;

            amberLight.enabled = false;
            yield return new WaitForSeconds(0.5f);
            amberLight.enabled = true;
            yield return new WaitForSeconds(0.5f);
            amberLight.enabled = false;
            yield return new WaitForSeconds(0.5f);
            amberLight.enabled = true;
            yield return new WaitForSeconds(0.5f);
            amberLight.enabled = false;
            yield return new WaitForSeconds(0.5f);
            amberLight.enabled = true;
            yield return new WaitForSeconds(0.5f);
            amberLight.enabled = false;

            greenLight.enabled = true;

            yield return new WaitForSeconds(fMaxActiveTime);

            bActive = false;
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
