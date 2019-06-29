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

    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        rotationalSpeed = 180.0f / (gm.leveltimer);
    }
    // Update is called once per frame
    void Update()
    {
            dayLightCool.Rotate(rotationDirection, Time.deltaTime * rotationalSpeed);
            dayLightWarm.Rotate(rotationDirection, Time.deltaTime * rotationalSpeed);
    }
}
