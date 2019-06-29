using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationDirection = new Vector3(0,0,1);

    [SerializeField]
    private float rotationalSpeed = 1.0f;

    [SerializeField]
    private Transform dayLightCool;
    [SerializeField]
    private Transform dayLightWarm;
    [SerializeField]
    private Transform NightLightCool;

    // Update is called once per frame
    void Update()
    {
        dayLightCool.Rotate(rotationDirection,Time.deltaTime * rotationalSpeed);
        dayLightWarm.Rotate(rotationDirection, Time.deltaTime * rotationalSpeed);

        if(dayLightCool.rotation.x > 180 || dayLightCool.rotation.x < 0)
        {
            dayLightCool.GetComponent<Light>().enabled = false;
            dayLightWarm.GetComponent<Light>().enabled = false;
        }
        else
        {
            dayLightCool.GetComponent<Light>().enabled = true;
            dayLightWarm.GetComponent<Light>().enabled = true;
        }
    }
}
