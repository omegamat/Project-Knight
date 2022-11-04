using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundsManager
{
    public enum Sounds
    {
        PlayerAttack,
        Attack,
        Hit,
        Jump,
        Menu,
    }
    public static void PlaySound(Sounds _sounds)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(_sounds));
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
}
