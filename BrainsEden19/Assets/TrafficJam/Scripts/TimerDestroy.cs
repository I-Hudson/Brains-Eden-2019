using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    [SerializeField]
    float Timer;

    private void Start()
    {
        StartCoroutine(CrashTimer());
    }

    IEnumerator CrashTimer()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    }
}
