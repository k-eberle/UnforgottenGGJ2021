using System;
using UnityEngine;

[Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volume;
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioData[] soundList;

    private AudioSource source;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        source = GetComponent<AudioSource>();
        instance = this;
    }

    public static void PlaySound(string name, float volumeFactor = 1.0f)
    {
        if (!instance)
        {
            Debug.LogError("AudioManager not found!");
            return;
        }

        foreach (AudioData sound in instance.soundList)
        {
            if (sound.name == name)
            {
                instance.source.PlayOneShot(sound.clip, Mathf.Pow(volumeFactor * sound.volume, 2.0f));
                return;
            }
        }
    }
}
