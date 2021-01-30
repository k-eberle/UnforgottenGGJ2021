using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
//#elif UNITY_WEBPLAYER
//         Application.OpenURL(webplayerQuitURL);
//#else
         Application.Quit();
#endif
    }
}
