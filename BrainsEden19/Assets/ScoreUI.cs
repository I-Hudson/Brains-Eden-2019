using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    [SerializeField]
    private TextMesh scoreCounter;

    [SerializeField]
    private CarColour thisCarColor;

    // Start is called before the first frame update
    void Start()
    {
        scoreSystem = GameObject.FindObjectOfType<ScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter.text = "" + scoreSystem.GetScore(thisCarColor);
    }
}
