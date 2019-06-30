using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    CarSpawner carSpawner;
    public CarColour PlayeColour;

    private void Start()
    {
        carSpawner = FindObjectOfType<CarSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car" && PlayeColour == other.GetComponent<CarUpdate>().CarColour && other.isTrigger)
        {
            carSpawner.RemoveCar();
            ScoreSystem.Instance.IncermentScore(PlayeColour, 2);
            Destroy(other.gameObject, 3.0f);
        }
        else if(other.tag == "Car" && PlayeColour != other.GetComponent<CarUpdate>().CarColour && other.isTrigger)
        {
            carSpawner.RemoveCar();
            ScoreSystem.Instance.RemoveScore(PlayeColour, 1);
            Destroy(other.gameObject, 3.0f);
        }
    }
}
