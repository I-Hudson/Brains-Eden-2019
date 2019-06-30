using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuController : MonoBehaviour
{
    private float StartTimer = 10.0f;
    [SerializeField]
    private TextMeshProUGUI tmpguiTimer;
    public void onClickStart()
    {
        SceneManager.LoadScene("MenuControls");
    }

    public void onClickQuit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuControls")
        {
            StartTimer -= Time.deltaTime;
            tmpguiTimer.text = "Game Starting in: " + Mathf.RoundToInt(StartTimer) + " seconds";
            if (StartTimer <= -1f)
            {
                SceneManager.LoadScene("GamePlayScene");
            }
        }
    }
}
