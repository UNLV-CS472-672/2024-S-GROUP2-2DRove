using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;

    public string tagName;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend;

    [Range(0f, 500f)]
    public float minDistance = 1f;

    [Range(1f, 500f)]
    public float maxDistance = 2f; 

    public bool loop;

    public AudioMixer audioMixer; 

    [HideInInspector]
    public AudioSource source;
}
