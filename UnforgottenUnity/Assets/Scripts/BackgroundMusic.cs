using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    public AudioSource track1;
    public AudioSource track2;

    public AudioData levelMenuTack1;
    public AudioData levelMenuTack2;
    public AudioData level1Track1;
    public AudioData level1Track2;
    public AudioData level2Track1;
    public AudioData level2Track2;
    public AudioData level3Track1;
    public AudioData level3Track2;
    public AudioData levelBossTrack1;
    public AudioData levelBossTrack2;

    private string activeScene;


    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
        activeScene = "";
    }

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != activeScene)
            SwitchMusic(currentScene);
    }

    private void SwitchMusic(string scene)
    {
        if (scene == "MainMenu")
            ActivateMusic(levelMenuTack1, levelMenuTack2);
        else if (scene == "Level1")
            ActivateMusic(level1Track1, level1Track2);
        else if (scene == "Level2")
            ActivateMusic(level2Track1, level2Track2);
        else if (scene == "Level3")
            ActivateMusic(level3Track1, level3Track2);
        else if (scene == "Boss")
            ActivateMusic(levelBossTrack1, levelBossTrack2);
        else
            ActivateMusic(null, null);

        activeScene = scene;
    }

    private void ActivateMusic(AudioData data1, AudioData data2)
    {
        track1.Stop();
        track2.Stop();

        if (data1 != null)
        {
            track1.clip = data1.clip;
            track1.volume = Mathf.Pow(data1.volume, 2.0f);
            track1.Play();
        }

        if (data2 != null)
        {
            track2.clip = data2.clip;
            track2.volume = Mathf.Pow(data2.volume, 2.0f);
            track2.Play();
        }
    }
}
