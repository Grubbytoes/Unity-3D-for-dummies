using UnityEngine;

class SpookyDoor : MonoBehaviour
{
    public Noisemaker sfxNoisemaker;

    protected Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();

        if (GameSingleton.S.SwitchHit) 
        {
            anim.SetBool("open", true);
        }
    }

    // TODO maybe make this a bit less obviously looping?
    public void StartDoorShake()
    {
        anim.SetBool("shakeDoor", true);
    }

    public void MakeNoise(int idx)
    {
        sfxNoisemaker.MakeNoise(idx);
    }
}