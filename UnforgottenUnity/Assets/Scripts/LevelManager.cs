using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public int nextScene;

    public List<string> storyMessages = new List<string>();
    int nextStoryTextIndex = 0;

    public GameObject ItemImage;

    public Canvas canvas;
    public TMP_Text text;

    bool seeGUI = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowNextText();
    }


    // Update is called once per frame
    void Update()
    {
        if (seeGUI && Input.anyKeyDown)
        {
            DisableText();
        }
    }

    public void ShowNextText()
    {
        canvas.enabled = true;
        text.text = storyMessages[nextStoryTextIndex];
        nextStoryTextIndex++;

        seeGUI = true;


        if(nextStoryTextIndex == storyMessages.Count)
        {
            this.ItemImage.SetActive(true);
        }

        Time.timeScale = 0.0f;
    }

    public void DisableText()
    {
        Time.timeScale = 1.0f;
        canvas.enabled = false;
        if (nextStoryTextIndex == storyMessages.Count)
        {
            SceneManager.LoadScene(nextScene);
        }
    }


}
