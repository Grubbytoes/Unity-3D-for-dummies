using UnityEngine;

public class Collectable : MonoBehaviour
{        
	public string ItemName = "collectable";
    public Noisemaker sfxNoisemaker;

    public virtual void OnCollectedBy(PlayerCharacter playerChar)
    {
        // Add reference to inventory
        playerChar.Inventory.Add(ItemName);

        // Play sound effect
        sfxNoisemaker.MakeNoise();

        // Deactivate self
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checking the collided object is a player
        var playerChar = other.GetComponent<PlayerCharacter>();
        if (playerChar == null) return;
        
        playerChar.OnCollect(this);
        OnCollectedBy(playerChar);
    }
}
