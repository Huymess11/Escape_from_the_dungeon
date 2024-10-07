using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioSource Music;

    public AudioClip music,endMusic;
    public AudioClip hit, attack, click, upgrade, unlock, game_over,open_chest,inventory;
    private void Awake()
    { 
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        PlayMusic(music);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        if (Music.clip == clip && Music.isPlaying)
        {
            return; 
        }
        Music.Stop(); 
        Music.clip = clip; 
        Music.loop = true;
        Music.Play(); 
    }
}
