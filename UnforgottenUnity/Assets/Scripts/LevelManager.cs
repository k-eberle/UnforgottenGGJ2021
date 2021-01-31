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

    public GameObject canvas;
    public TMP_Text text;

    bool seeGUI = false;
    public bool startWithText;

    private bool showAdditionalScreen = false;
    public string additionalText = "";

    // Start is called before the first frame update
    void Start()
    {
        if (startWithText)
        {
            ShowNextText();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (seeGUI && Input.anyKeyDown && !showAdditionalScreen)
        {
            DisableText();
        }
        else if (seeGUI && Input.anyKeyDown && showAdditionalScreen)
        {
            text.text = additionalText;
            showAdditionalScreen = false;
        }
    }

    public void ShowNextText()
    {
        canvas.SetActive(true);
        text.text = storyMessages[nextStoryTextIndex];
        nextStoryTextIndex++;

        seeGUI = true;


        if(nextStoryTextIndex == storyMessages.Count)
        {
            this.ItemImage.SetActive(true);
        }

        Time.timeScale = 0.0f;
    }

    public void ShowCustomText(string customText)
    {
        canvas.SetActive(true);
        text.text = customText;
        nextStoryTextIndex++;

        seeGUI = true;


        this.ItemImage.SetActive(false);

        Time.timeScale = 0.0f;
    }

    public void ShowCustomTextNext(string customText)
    {
        showAdditionalScreen = true;
        additionalText = customText;

    }

    public void DisableText()
    {
        Time.timeScale = 1.0f;
        canvas.SetActive(false);
        //if (nextStoryTextIndex == storyMessages.Count)
        //{
        //    SceneManager.LoadScene(nextScene);
        //}
    }

    public void LoadNextScene()
    {
            SceneManager.LoadScene(nextScene);
    }

}
