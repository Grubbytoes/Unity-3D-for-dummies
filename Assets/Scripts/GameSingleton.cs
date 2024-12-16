using UnityEngine;
using UnityEngine.SceneManagement;


// I, Hugo Abbott, promise not to abuse this singleton and only use it if absolutely necessary.
// I also acknowledge that if I was more knowledgeable about unity I would probably not be doing this and would
// be able to come up with a much more elegant solution.

class GameSingleton : MonoBehaviour
{
    private bool _switchHit;
    public bool SwitchHit
    {
        get { return _switchHit; }
        protected set { _switchHit = value; }
    }

    private string _enteredFrom;
    public string EnteredFrom
    {
        get { return _enteredFrom; }
        private set { _enteredFrom = value; }
    }

    public static readonly GameSingleton S = new();

    public static void EnsureSingleton()
    {   
        // This is very bad and I hate it forgive me steve       
    }

    public GameSingleton()
    {
        if (this != S) 
        {
            Debug.Log("Killed rouge singleton");
        }
        Debug.Log("Game singleton instanced!!");
    }

    public bool ChangeScene(string from, string to)
    {
        // var oldScene = SceneManager.GetSceneByName(from);
        // var newScene = SceneManager.GetSceneByName(to);

        // // Guard against invalid scenes
        // if (!(oldScene.IsValid() && newScene.IsValid())) 
        // {
        //     if (!oldScene.IsValid()) Debug.Log($"Bad scene name '{from}'");
        //     if (!newScene.IsValid()) Debug.Log($"Bad scene name '{to}'");
            
        //     return false;
        // }

        // // We know now have a valid scene which is safe to load

        // Apparently there is no error checking I can do I fucking love unity so much aaaaaah
        EnteredFrom = from;
        SceneManager.LoadScene(to);
        return true;
    }

    public void HitSwitch()
    {
        SwitchHit = true;
    }
}