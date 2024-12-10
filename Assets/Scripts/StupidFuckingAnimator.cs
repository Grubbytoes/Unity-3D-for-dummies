using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidFuckingAnimator : MonoBehaviour
{
    public Animator anim;
    protected float offset;

    void Awake() {
        System.Random r = new();
        offset = (float)r.NextDouble();  
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("flickerOffset", offset);
        anim.SetFloat("flickerSpeed", 1+offset/2);
        anim.SetBool("isFlickering", true);
    }
}
