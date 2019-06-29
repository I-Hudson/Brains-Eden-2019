using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private static ScoreSystem instance = null;
    public static ScoreSystem Instance
    { get { return instance; } }

    private Dictionary<CarColour, int> PlayerScore = new Dictionary<CarColour, int>();

    private void Awake()
    {
        if(instance == null)
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
        if(!PlayerScore.ContainsKey(aPlayer))
        {
            PlayerScore.Add(aPlayer, aScoreToAdd);
        }
        else
        {
            PlayerScore[aPlayer] += 1;
        }
    }
}
