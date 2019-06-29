using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car")]
public class Car : ScriptableObject
{
    public GameObject Model;
    public float MaxSpeed;
    public float ControlSpeed;
    public float MinHitDistance;
    public Color Colour;
    public CarType CarType;
    public string Tag;
    
}
