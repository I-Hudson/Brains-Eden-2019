using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationDirection = new Vector3(0,0,1);

    [SerializeField]
    private float rotationalSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, rotationDirection,Time.deltaTime * rotationalSpeed);
    }
}
