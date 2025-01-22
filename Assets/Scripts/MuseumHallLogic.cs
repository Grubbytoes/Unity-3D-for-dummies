using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumHallLogic : MonoBehaviour
{
    [SerializeField] protected PlayerCharacter playerCharacter;
    [SerializeField] protected Transform altSpawn;
    [SerializeField] protected GameObject enemyDinosaur;

    // Start is called before the first frame update
    void Start()
    {
        enemyDinosaur.SetActive(false);

        Debug.Log(GameSingleton.S.EnteredFrom);

        if (GameSingleton.S.EnteredFrom == "Cellar")
        {
            playerCharacter.transform.position = altSpawn.position; // I have no clue why this does not work
            playerCharacter.transform.rotation = altSpawn.rotation;
            if (GameSingleton.S.SwitchHit) PostHitSwitch();
        }
        else 
        {
            Debug.Log("did not enter from cellar");
        }
    }

    protected void PostHitSwitch()
    {
        Debug.Log("Re-entered museum after hitting switch");
        enemyDinosaur.SetActive(true);
    }
}
