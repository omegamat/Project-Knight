using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();    
            } 
                
            return _i;
        }
    }


    public AudioMixerGroup audioMixerGroup_Music;
    public AudioMixerGroup audioMixerGroup_SoundEffect;
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        [HideInInspector]
        public AudioSource source;

        public SoundsManager.Sounds sounds;
        public AudioClip audioClip;
        public AudioMixerGroup audioMixerGroup;
        [Range(0f,1f)]
        public float volume = 1;
        [Range(.1f,3f)]
        public float pitch = 1;
        public bool loop;
        public bool playOnAwake = true;

    }

    public GameObject gems;
    public ParticleSystem gemsParticle;
    public ParticleSystem Smoke_particule;
}
