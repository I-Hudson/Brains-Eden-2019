using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looptrigger : MonoBehaviour
{
    [SerializeField]
    List<GameObject> waitingCars;

    public TriggerStay LoopArea;

    public Transform Loop;

    private void Start()
    {
        StartCoroutine(LoopCars());
    }

    private IEnumerator LoopCars()
    {
        while (true)
        {
            if (waitingCars.Count > 0 && !LoopArea.InTrigger)
            {
                GameObject go = waitingCars[0];
                go.SetActive(true);
                go.transform.position = Loop.position;
                go.transform.rotation = Loop.rotation;

                waitingCars.Remove(go);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            waitingCars.Add(other.gameObject);
            waitingCars[waitingCars.Count - 1].SetActive(false);
        }
    }
}
