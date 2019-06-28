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
        while (true)
        {
            horizontalLane1.enabled = !horizontalLane1.enabled;
            horizontalLane1_1.enabled = !horizontalLane1_1.enabled;

            horizontalLane2.enabled = !horizontalLane2.enabled;
            horizontalLane2_2.enabled = !horizontalLane2_2.enabled;

            yield return new WaitForSeconds(5.0f);
        }
    }
}
