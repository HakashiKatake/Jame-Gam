using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _Audio;
    bool on = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
            _Audio.Play();
    }

    public void Play()
    {
        if (!on)
        {
            PlayerPrefs.SetInt("Music", 1);
            _Audio.Play();
            on = true;
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            _Audio.Stop();
            on = false;
        }
    }
}
