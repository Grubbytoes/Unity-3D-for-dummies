using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampAnimator : MonoBehaviour
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

        if (GameSingleton.S.EnteredFrom == "Cellar")
        {
            anim.SetBool("isEvil", true);
        }
        else
        {
            anim.SetBool("isFlickering", true);
        }
    }
}
