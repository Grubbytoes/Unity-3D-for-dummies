using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    protected AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public bool MakeNoise()
    {
        return MakeNoise(0);
    }

    public bool MakeNoise(int idx)
    {
        if (idx >= 0 && idx < audioSources.Length)
        {
            audioSources[idx].Play();
            return true;
        }
        else
        {
            Debug.Log($"Audio source {idx} out of bounds");
            return false;
        }
    }
}
