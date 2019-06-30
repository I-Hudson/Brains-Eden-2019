using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI debugTextUI;

    [SerializeField]
    private float maxResetTimer = 3.0f;

    [SerializeField]
    public float levelMaxTime = 3.0f; // in mins

    public float leveltimer = 3;

    [SerializeField]
    private float tempTimer = 0;

    public bool resetting = false;


    // Start is called before the first frame update
    void Awake()
    {
        levelMaxTime *= 60.0f;
        leveltimer = levelMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(leveltimer <= 0 && !resetting)
        {
            debugTextUI.text = "Press A to Restart! \n Press B to Quit";

            for (int i = 0; i < 4; i++)
            {
                if (Input.GetKeyDown("joystick " + (i + 1) + " button 0"))
                {
                    StartCoroutine(ResetLevel());
                }
                if (Input.GetKeyDown("joystick " + (i + 1) + " button 1"))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
        else if(leveltimer > 0 && !resetting)
        {
            leveltimer -= Time.deltaTime;
            debugTextUI.text = "Time Remaining: " + Mathf.Round(leveltimer) + "secs";
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine( ResetLevel());
        }
    }

    private IEnumerator ResetLevel()
    {
        resetting = true;
        for (int i = (int)maxResetTimer; i > 0; i--)
        {
            debugTextUI.text = "Resetting Scene in: " + i + " ";
            yield return new WaitForSeconds(1.0f);
        }
        SceneManager.LoadScene("GamePlayScene");
    }
}
