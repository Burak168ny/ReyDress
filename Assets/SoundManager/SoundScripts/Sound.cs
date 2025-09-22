using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    [Range(0,3)] public float volume = 0.3f;
    [Range(0,2)] public float pitch = 2f;
    [HideInInspector] public AudioSource audioSource;
    public bool loop;
}
