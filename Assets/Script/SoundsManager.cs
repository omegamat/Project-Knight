using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundsManager
{
    public enum Sounds
    {
        PlayerAttack,
        Attack,
        Hit,
        Jump,
        Menu,
        Pickup
    }
    public static void PlaySound(Sounds _sounds)
    {
        /*
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource;
        audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = GameAssets.i.audioMixerGroup_SoundEffect;
        audioSource.PlayOneShot(GetAudioClip(_sounds));
        */

        GetAudioSource(_sounds).Play();
    }

    private static AudioClip GetAudioClip(Sounds _sounds)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sounds == _sounds)
            {               
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sounds: " + _sounds + " not found");
        return null;
    }
    private static AudioSource GetAudioSource(Sounds _sounds)
    {
        foreach (GameAssets.SoundAudioClip s in GameAssets.i.soundAudioClipArray)
        {
            if (s.sounds == _sounds)
            {
                GameObject soundGameObject = new GameObject("Sound");
                s.source = soundGameObject.AddComponent<AudioSource>(); 
                s.source.clip = s.audioClip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.audioMixerGroup;           
                return s.source;
            }
        }
        Debug.LogError("Sounds: " + _sounds + " not found");
        return null;
    }
}
