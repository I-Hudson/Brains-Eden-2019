using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStay : MonoBehaviour
{
    public bool InTrigger;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Car")
        {
            InTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
        {
            InTrigger = false;
        }
    }
}
