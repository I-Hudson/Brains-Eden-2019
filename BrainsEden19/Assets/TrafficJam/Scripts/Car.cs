using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car")]
public class Car : ScriptableObject
{
    public GameObject Model;
    public float Speed;
    public Color Colour;
    public string Tag;
}
