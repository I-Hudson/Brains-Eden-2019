using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeedback : MonoBehaviour
{
    [SerializeField]
    GameObject positiveFeedback;
    [SerializeField]
    GameObject negativeFeedback;

    [SerializeField]
    CarColour Colour;

    [SerializeField]
    Transform spawnPosition;

    List<bool> positive = new List<bool>();


    private void Start()
    {
        StartCoroutine(SpawnFeedback());
    }

    private IEnumerator SpawnFeedback()
    {
        while (true)
        {
            if (positive.Count > 1)
            {
                SpawnFeedback(positive[0]);
                positive.RemoveAt(0);
                yield return new WaitForSeconds(0.5f);
            }
            else if (positive.Count == 1)
            {
                SpawnFeedback(positive[0]);

                positive.RemoveAt(0);
                yield return null;
            }
            yield return null;
        }
    }
    private void SpawnFeedback(bool aPositive)
    {
        if (aPositive)
        {
            GameObject go = Instantiate(positiveFeedback);
            go.transform.position = spawnPosition.position;
            Destroy(go, 1.5f);
        }
        else
        {
            GameObject go = Instantiate(negativeFeedback);
            go.transform.position = spawnPosition.position;
            Destroy(go, 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car" && other.isTrigger)
        {
            if (other.GetComponent<CarUpdate>().CarColour == Colour)
            {
                positive.Add(true);

            }
            else if(other.GetComponent<CarUpdate>().CarColour != Colour)
            {
                positive.Add(false);
            }
        }
    }
}
