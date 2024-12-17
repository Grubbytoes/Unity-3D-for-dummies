using UnityEngine;
using UnityEngine.SceneManagement;

public class CellerRoomLogic : MonoBehaviour
{    
    public void ExitToHall()
    {
        SceneManager.LoadScene("MuseumHall");
    }
}
