using UnityEngine;

public class CutsceneTimer : MonoBehaviour
{
    public float time = 10;
    public string currentScene;
    public string nextScene;

    void Update()
    {
        if (time <= 0)
        {
            GameSingleton.S.ChangeScene(currentScene, nextScene);
            return;
        }

        time -= Time.deltaTime;
    }
}