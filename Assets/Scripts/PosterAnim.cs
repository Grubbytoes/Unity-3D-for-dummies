using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterAnim : HighlightObject
{
    public Noisemaker sfxNoisemaker;

    public void DropPoster() {
        var anim = GetComponent<Animator>();
        anim.SetTrigger("drop");
        sfxNoisemaker.MakeNoise();
    }
}
