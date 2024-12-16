using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public void DropPoster() {
        var anim = GetComponent<Animator>();
        anim.SetTrigger("drop");
    }
}
