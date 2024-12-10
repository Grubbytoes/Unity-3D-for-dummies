using UnityEngine;

class SpookyDoor : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource audioSource;

    void Awake() {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartDoorShake() {
        anim.SetBool("shakeDoor", true);
    }

    public void MakeNoise(string soundKey) {
        // TODO
        Debug.Log($"Sound be playing sound {soundKey} - not yet implimented");
    }
}