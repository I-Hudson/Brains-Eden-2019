using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTrafficLight : MonoBehaviour
{
    [SerializeField]
    private Collider horizontalLane1;
    [SerializeField]
    private Collider horizontalLane1_1;

    [SerializeField]
    private Collider horizontalLane2;
    [SerializeField]
    private Collider horizontalLane2_2;

    [SerializeField]
    private Collider center;

    [SerializeField]
    float switchTimer;
    Coroutine coro;

    // Start is called before the first frame update
    void Start()
    {
        coro = StartCoroutine(Switch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Switch()
    {
        bool switchHor = true;
        while (true)
        {
            horizontalLane1.enabled = true;
            horizontalLane1_1.enabled = true;

            horizontalLane2.enabled = true;
            horizontalLane2_2.enabled = true;

            bool carsInCenter = true;
            while(carsInCenter)
            { 
                carsInCenter = false;
                GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

                for (int i = 0; i < cars.Length; i++)
                {

                    if (center.bounds.Contains(cars[i].transform.position))
                    {
                        carsInCenter = true;
                        yield return null;
                    }
                }
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);

            horizontalLane1.enabled = switchHor;
            horizontalLane1_1.enabled = switchHor;

            horizontalLane2.enabled = !switchHor;
            horizontalLane2_2.enabled = !switchHor;

            switchHor = !switchHor;

            yield return new WaitForSeconds(switchTimer);
        }
    }
}
