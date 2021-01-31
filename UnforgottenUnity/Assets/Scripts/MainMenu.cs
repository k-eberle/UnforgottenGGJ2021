using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel1()
    {
        StartCoroutine(LoadScene("Level1"));
    }

    public void LoadLevel2()
    {
        StartCoroutine(LoadScene("Level2"));
    }

    public void LoadLevel3()
    {
        StartCoroutine(LoadScene("Level3"));
    }

    public void LoadBoss()
    {
        StartCoroutine(LoadScene("Boss"));
    }

    private IEnumerator LoadScene(string name)
    {
        yield return new WaitForSeconds(0.23f);
        SceneManager.LoadScene(name);
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
