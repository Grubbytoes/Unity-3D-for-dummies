using UnityEngine;
using UnityEngine.SceneManagement;

public class CellerRoomLogic : MonoBehaviour
{    
    public void ExitToHall()
    {
        GameSingleton.S.ChangeScene("Cellar", "MuseumHall");
    }

    public void HitSwitch()
    {
        GameSingleton.S.HitSwitch();
    }
}
