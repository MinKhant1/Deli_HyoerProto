using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource _musicSource, _sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void PlaySoundAndVibrate(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
        Handheld.Vibrate();
    }


}
