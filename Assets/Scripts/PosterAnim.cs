using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterAnim : MonoBehaviour
{
    protected AudioSource audio;

    // Start is called before the first frame update
    void Start() 
    {
        audio = GetComponent<AudioSource>();
    }

    public void DropPoster() {
        var anim = GetComponent<Animator>();
        anim.SetTrigger("drop");
        audio.Play();
    }
}
