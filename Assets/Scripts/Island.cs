using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Island : MonoBehaviour
{
    public static Island Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool CollectedAllDocs = false;
    [Space]
    [SerializeField] float TimeUntilLand;
    [SerializeField] TextMeshPro Timer;
    float timer;
    bool startTimer;
    [Space]
    [SerializeField] string CreditsScene = "Credits";

    private void Update()
    {
        if (startTimer)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                timer = 9999;
                Timer.enabled = false;

                GameManager.Instance.SaveScore();

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                TransitionPanel.instance.TransitionScene(CreditsScene);
            }
            int inttime = (int)timer;
            Timer.text = inttime.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && CollectedAllDocs)
        {
            startTimer = true;
            timer = TimeUntilLand;
            Timer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && CollectedAllDocs)
        {
            startTimer = false;
            Timer.enabled = false;
        }
    }
}
