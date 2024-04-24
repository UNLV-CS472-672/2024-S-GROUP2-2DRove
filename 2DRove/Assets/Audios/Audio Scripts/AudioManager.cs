using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // foreach (Sound s in sounds)
        // {
        //     s.source = gameObject.AddComponent<AudioSource>();
        //     s.source.clip = s.clip;
        //     s.source.volume = s.volume;
        //     s.source.pitch = s.pitch;
        //     s.source.spatialBlend = s.spatialBlend;
        //     s.source.minDistance = s.minDistance;
        //     s.source.maxDistance = s.maxDistance;
        //     s.source.loop = s.loop;
        //     s.source.outputAudioMixerGroup = s.audioMixer.FindMatchingGroups("Master")[0];
        // }
    }

    public void AddAudioSourcesToEnemy(GameObject gameObj, string objectName)
    {
        // Find all sounds for this enemy type
        Sound[] enemySounds = Array.FindAll(sounds, s => s.tagName == objectName);
        foreach (Sound s in enemySounds)
        {
            if (!(gameObj.GetComponents<AudioSource>().Any(a => a.clip == s.clip)))
            {            
                AudioSource source = gameObj.AddComponent<AudioSource>();
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.spatialBlend = s.spatialBlend;
                source.minDistance = s.minDistance;
                source.maxDistance = s.maxDistance;
                source.loop = s.loop;
                source.outputAudioMixerGroup = s.audioMixer.FindMatchingGroups("Master")[0];

            }
        }
    }

    void Start()
    {
        // Play("Main Menu");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
