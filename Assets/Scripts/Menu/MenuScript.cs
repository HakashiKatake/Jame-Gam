using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] string GameSceneName = "Game";
    [SerializeField] string MenuSceneName = "Start Menu";
    [SerializeField] GameObject SittingsMenu;
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] TextMeshProUGUI Highscore;

    private void Awake()
    {
        if (SittingsMenu != null)
            SittingsMenu.SetActive(false);
    }

    private void Start()
    {
        if (Score != null)
        {
            Score.text = PlayerPrefs.GetInt("Score").ToString();
        }
        Highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void StartGame()
    {
        TransitionPanel.instance.TransitionScene(GameSceneName);
    }

    public void BackToMenu()
    {
        TransitionPanel.instance.TransitionScene(MenuSceneName);
    }

    public void OpenSittingMenu()
    {
        if (SittingsMenu != null)
            SittingsMenu.SetActive(true);
    }

    public void CloseSittingMenu()
    {
        if (SittingsMenu != null)
            SittingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit from game");
        Application.Quit();
    }
    
}
