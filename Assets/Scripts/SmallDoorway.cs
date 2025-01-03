using UnityEngine;

// God have mercy on me a sinner, I am loosing my mind
public class SmallDoorway : MonoBehaviour
{
    protected Animator anim;
    protected bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TryGoThrough()
    {
        if (!isOpen) return;

        GameSingleton.S.ChangeScene("MuseumHall", "Cellar");
    }

    public void OpenDoor()
    {
        anim.SetTrigger("openDoor");
        isOpen = true;
    }
}
