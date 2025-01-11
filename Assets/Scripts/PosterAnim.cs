using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterAnim : HighlightObject
{
    protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DropPoster() {
        var anim = GetComponent<Animator>();
        anim.SetTrigger("drop");
        audioSource.Play();
    }
}
