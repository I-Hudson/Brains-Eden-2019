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
        if(other.tag == "Car" && PlayeColour == other.GetComponent<CarUpdate>().CarColour)
        {
            carSpawner.RemoveCar();
            ScoreSystem.Instance.IncermentScore(other.gameObject.GetComponent<CarUpdate>().CarColour, 1);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Car" && PlayeColour != other.GetComponent<CarUpdate>().CarColour)
        {

        }
    }
}
