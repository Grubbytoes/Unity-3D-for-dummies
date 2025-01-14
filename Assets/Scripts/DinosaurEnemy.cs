using UnityEngine;

public class DinosaurEnemy : BaseCharacter
{
    public Transform targetPosition;

    void Update()
    {
        intendedMoveDir = CalculateMoveDir() * moveSpeed;

        MoveCycle();
        SfxCycle();
    }

    public void PlayerCaught()
    {
        Debug.Log("I have caught the player!");
        GameSingleton.S.ChangeScene("MuseumHall", "Death");
    }

    private Vector2 CalculateMoveDir()
    {
        var thisPosition = GetComponent<Transform>().position;

        Vector2 currentlyAt = new(thisPosition.x, thisPosition.z);
        Vector2 goingTowards = new(targetPosition.position.x, targetPosition.position.z);

        return (goingTowards - currentlyAt).normalized;
    }
}