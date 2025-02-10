using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public int Score;
    float timeCompleted;
    [Space]
    public int DocumentsBuildings;
    public int FuelBuildings;
    [Space]
    public int DocsCollected = 0;
    [SerializeField] TextMeshProUGUI DocText;
    [Space]
    [SerializeField] GameObject MissionOne;
    [SerializeField] GameObject MissionTwo;

    private void Start()
    {
        MissionOne.SetActive(true);
        MissionTwo.SetActive(false);
    }

    private void Update()
    {
        timeCompleted += Time.deltaTime / 5f;

        if (Input.GetKeyDown(KeyCode.M))
            BackToMenu();
    }

    void BackToMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        TransitionPanel.instance.TransitionScene("Start Menu");
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", Score / (int)timeCompleted);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Highscore"))
            PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Score"));
    }

    public void CollectDoc()
    {
        DocsCollected++;
        DocText.text = DocsCollected.ToString();
        if (DocsCollected == DocumentsBuildings)
        {
            MissionOne.SetActive(false);
            MissionTwo.SetActive(true);

            Island.Instance.CollectedAllDocs = true;

            Scanner.instance.SpawnPermArrow(Island.Instance.transform);
        }
    }

}
