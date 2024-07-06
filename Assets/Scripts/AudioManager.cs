using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    private AudioSource src;

    public const int SFX = 0;
    public const int MUSIC = 1;
    public const int AMBIENCE = 2;

    void Start() {
        src = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, int type) {
        src.clip = clip;
        src.Play();
    }
}
