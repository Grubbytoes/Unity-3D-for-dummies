using UnityEngine;
using UnityEngine.SceneManagement;

public class CellerRoomLogic : MonoBehaviour
{    
    public void ExitToHall()
    {
        Debug.Log("Exiting to hall");
        SceneManager.LoadScene("MuseumHall");
    }
}
