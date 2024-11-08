using UnityEngine;

public class Collectable : MonoBehaviour
{    
    private Collider _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public virtual void OnCollectedBy(BasePlayerCharacter pLayerChar)
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checking the collided object is a player
        var playerChar = other.GetComponent<BasePlayerCharacter>();
        if (playerChar == null) return;
        
        playerChar.OnCollect(this);
        OnCollectedBy(playerChar);
    }
}
