using UnityEngine.Audio;
using System;
using UnityEngine;

//audio manager is for music not fix in a objects
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    private void Awake() 
    {
        foreach(Sound s in sounds)
        {
          s.source = gameObject.AddComponent<AudioSource>();
          s.source.clip = s.clip;

          s.source.volume = s.volume;
          s.source.pitch = s.pitch;
          s.source.loop = s.loop;

        }
    }
        
    private void Start() {
        Play("Theme");
    }

    public void Play (string _name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s.name == null)
            Debug.Log("Sound With name '" + "' Not found");
            return;
        s.source.Play();
    }
}
