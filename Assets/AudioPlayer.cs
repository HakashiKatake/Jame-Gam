using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] AudioSource _Audio;
    [Space]
    [SerializeField] AudioClip PowerUp;
    [SerializeField] AudioClip Paper;

    public void PlayPowerUp()
    {
        _Audio.volume = 0.1f;
        _Audio.PlayOneShot(PowerUp);
    }

    public void PlayPaper()
    {
        _Audio.volume = 0.4f;
        _Audio.PlayOneShot(Paper);
    }
}
