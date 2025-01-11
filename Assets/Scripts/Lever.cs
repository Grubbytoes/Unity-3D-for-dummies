using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : InteractObject
{
    [SerializeField] protected UnityEvent onPulled;
    protected Animator anim;
    protected bool pulled = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Could not find animator for lever");
        }
        else
        {
            Debug.Log("Animator found");
        }
    }

    protected override void OnSelect()
    {
        if (pulled) return;

        anim.SetTrigger("pull");
        onPulled.Invoke();
        pulled = true;
    }
}
