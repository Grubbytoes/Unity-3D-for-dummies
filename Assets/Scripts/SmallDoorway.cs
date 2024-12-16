using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// God have mercy on me a sinner, I am loosing my mind
public class SmallDoorway : MonoBehaviour
{
    protected Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerCharacter.CollectedEnoughItems += OpenDoor;
    }

    private void OpenDoor()
    {
        Debug.Log("Door has opened");
        anim.SetTrigger("openDoor");
    }
}
