using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private static ScoreSystem instance = null;
    public static ScoreSystem Instance
    { get { return instance; } }

    [SerializeField]
    GameObject crashPrefab;
    int crashI = 0;


    private Dictionary<CarColour, int> PlayerScore = new Dictionary<CarColour, int>();
    int scoreI = 0;
    int scoreD = 0;

    [SerializeField]
    private AudioCenter ac;
    [SerializeField]
    private AudioClip plussound;
    [SerializeField]
    private AudioClip minussound;

    private void Awake()
    {
        ac = GameObject.FindObjectOfType<AudioCenter>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void IncermentScore(CarColour aPlayer, int aScoreToAdd)
    {
        ac.PlayLayered(plussound);
        if (!PlayerScore.ContainsKey(aPlayer))
        {
            PlayerScore.Add(aPlayer, aScoreToAdd);
        }
        else
        {
            PlayerScore[aPlayer] += aScoreToAdd;
        }
    }

    public void RemoveScore(CarColour aPlayer, int aScoreToRemove)
    {
        ac.PlayLayered(minussound);
        if (!PlayerScore.ContainsKey(aPlayer))
        {
            PlayerScore.Add(aPlayer, -aScoreToRemove);
        }
        else
        {
            PlayerScore[aPlayer] -= aScoreToRemove;
        }
    }

    public void Crash(Vector3 aPosition)
    {
        if(crashI == 0)
        {
            GameObject go = GameObject.Instantiate(crashPrefab);
            go.transform.position = aPosition;

            crashI += 1;
        }
        else if(crashI == 1)
        {
            crashI = 0;
        }
    }

    public int GetScore(CarColour color)
    {
        if (PlayerScore.ContainsKey(color))
        {
            return PlayerScore[color];
        }
        return 0;
    }
}
