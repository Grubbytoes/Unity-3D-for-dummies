using UnityEngine;

class SpookyDoor : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource[] audioSources;

    void Awake() {
        anim = GetComponent<Animator>();
        audioSources = GetComponents<AudioSource>();
    }

    // TODO maybe make this a bit less obviously looping?
    public void StartDoorShake() {
        anim.SetBool("shakeDoor", true);
    }

    public void MakeNoise(int idx) {
        if (idx == 0)
        {
            audioSources[idx].Play();
        }
        else if (idx == 1)
        {
            // Adding a bit of random here, as constant roaring was a little overbearing
            if (Random.value <= 0.4) return;
            audioSources[idx].Play();
        }
    }
}