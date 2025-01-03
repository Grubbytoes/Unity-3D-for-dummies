using Unity.VisualScripting;
using UnityEngine.Events;

public class SmallDoorCutsceneScript : CutsceneScript
{
    void Start()
    {
        PlayerCharacter.CollectedEnoughItems += Play;
    }

    protected override void Cutscene()
    {
        director.Play();
    }
}
