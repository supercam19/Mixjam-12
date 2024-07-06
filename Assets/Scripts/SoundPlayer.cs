using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public static List<AudioSource> sfxSources = new List<AudioSource>();
    public static List<AudioSource> musicSources = new List<AudioSource>();

    private static AudioSource prefab = Resources.Load<AudioSource>("Prefabs/SoundPlayer");

    public const int SFX = 0;
    public const int MUSIC = 1;
    public const int MASTER = 2;
    
    public static void Play(AudioClip clip, bool isMusic = false) {
        AudioSource src = MakeSource(isMusic);
        src.clip = clip;
        src.Play();
        RemoveReference(isMusic, src);
        Destroy(src.gameObject, src.clip.length);
    }
    
    public static void Play(AudioClip[] clips, bool isMusic = false) {
        AudioSource src = MakeSource(isMusic);
        src.clip = clips[Random.Range(0, clips.Length)];
        src.Play();
        RemoveReference(isMusic, src);
        Destroy(src.gameObject, src.clip.length);
    }

    public static void PlayRandomPitched(AudioClip clip, float variance = 0.1f, bool isMusic = false) {
        AudioSource src = MakeSource(isMusic);
        src.clip = clip;
        src.pitch = Random.Range(1 - variance, 1 + variance);
        src.Play();
        RemoveReference(isMusic, src);
        Destroy(src.gameObject, src.clip.length);
    }
    
    public static void PlayRandomPitched(AudioClip[] clips, float variance = 0.1f, bool isMusic = false) {
        AudioSource src = MakeSource(isMusic);
        src.clip = clips[Random.Range(0, clips.Length)];
        src.pitch = Random.Range(1 - variance, 1 + variance);
        src.Play();
        RemoveReference(isMusic, src);
        Destroy(src.gameObject, src.clip.length);
    }
    
    public static void PlayAtVolume(AudioClip clip, float volume) {
        AudioSource src = MakeSource(false);
        src.clip = clip;
        src.volume = volume;
        src.Play();
        RemoveReference(false, src);
        Destroy(src.gameObject, src.clip.length);
    }

    public static void SetVolume(int type, float volume) {
        if (type == MUSIC) {
            GameInfo.musicVolume = volume;
            GameInfo.musicVolume = volume;
        }
        else if (type == SFX) {
            GameInfo.sfxVolume = volume;
            GameInfo.sfxVolume = volume;
        }
        else if (type == MASTER) {
            GameInfo.masterVolume = volume;
            GameInfo.masterVolume = volume;
        }
        foreach (AudioSource src in type == MUSIC ? musicSources : sfxSources) {
            src.volume = volume * GameInfo.masterVolume;
        }
    }

    private static AudioSource MakeSource(bool isMusic) {
        AudioSource src = Instantiate(prefab);
        if (isMusic) {
            musicSources.Add(src);
            src.volume = GameInfo.musicVolume * GameInfo.masterVolume;
        }
        else {
            sfxSources.Add(src);
            src.volume = GameInfo.sfxVolume * GameInfo.masterVolume;
        }

        return src;
    }

    private static void RemoveReference(bool isMusic, AudioSource src) {
        if (isMusic) {
            musicSources.Remove(src);
        }
        else {
            sfxSources.Remove(src);
        }
    }
}
