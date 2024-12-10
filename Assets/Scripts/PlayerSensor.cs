using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSensor : MonoBehaviour
{
    public UnityEvent<PlayerCharacter> PlayerEntered;

    private void OnTriggerEnter(Collider other) 
    {
        var player = other.GetComponent<PlayerCharacter>();
        if (player == null) return;
        PlayerEntered.Invoke(player);
    }
}
