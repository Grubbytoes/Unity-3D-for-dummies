using System;
using UnityEngine;
using UnityEngine.Playables;

public abstract class CutsceneScript : MonoBehaviour
{
    public bool disablePlayerDuringCutscene = true;

    protected PlayableDirector director;
    [SerializeField] private PlayerCharacter playerCharacter;


    void Awake()
    {
        director = GetComponent<PlayableDirector>();

        if (disablePlayerDuringCutscene) director.stopped += OnStop;
    }
    protected void Play()
    {
        Debug.Log("Cutscene Starting");

        if (disablePlayerDuringCutscene) playerCharacter.DisableControl();
        Cutscene();
    }

    protected abstract void Cutscene(); 

    private void OnStop(PlayableDirector director)
    {
        if (disablePlayerDuringCutscene) playerCharacter.EnableControl();

        Debug.Log("Cutscene Over");
    }

}