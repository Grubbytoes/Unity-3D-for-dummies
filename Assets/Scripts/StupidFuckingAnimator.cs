using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidFuckingAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.Random r = new();
        Animator stupidIHateIt = GetComponent<Animator>();
        float offset = (float)r.NextDouble();

        // stupidIHateIt.SetFloat("Offset", offset);
        stupidIHateIt.SetTrigger("Go");
    }
}
