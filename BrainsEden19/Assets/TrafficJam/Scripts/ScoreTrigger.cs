using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    CarSpawner carSpawner;

    private void Start()
    {
        carSpawner = FindObjectOfType<CarSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            carSpawner.RemoveCar();
            ScoreSystem.Instance.IncermentScore(other.gameObject.GetComponent<CarUpdate>().CarColour, 1);
            Destroy(other.gameObject);
        }
    }
}
