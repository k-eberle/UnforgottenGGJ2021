﻿using System.Collections;
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
    public GameObject menu;
    public GameObject dialogue;
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

        if (PlayerPrefs.GetInt("LoadSave") > 0)
        {
            Vector3 newPos = new Vector3(PlayerPrefs.GetFloat("SaveX"), PlayerPrefs.GetFloat("SaveY"), PlayerPrefs.GetFloat("SaveZ"));
            GameObject.FindObjectOfType<PlayerController>().transform.position = newPos;
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
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

    public void ShowCustomText(string customText, float timer)
    {
        StartCoroutine(ShowCustomTextIn(customText, timer));
    }

    IEnumerator ShowCustomTextIn(string customText, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShowCustomText(customText);
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
        this.seeGUI = false;
        //if (nextStoryTextIndex == storyMessages.Count)
        //{
        //    SceneManager.LoadScene(nextScene);
        //}
    }

    public void LoadNextScene()
    {
        PlayerPrefs.SetInt("LoadSave", 0);
        SceneManager.LoadScene(nextScene);
    }

    public void ShowMenu()
    {

        Time.timeScale = 0.0f;
        canvas.SetActive(true);
        dialogue.SetActive(false);
        menu.SetActive(true);
    }

    public void DisableMenu()
    {
        Time.timeScale = 1.0f;       
        dialogue.SetActive(true);
        menu.SetActive(false);
        canvas.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        canvas.SetActive(true);
        Application.Quit();
#endif
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetInt("LoadSave", 0);
        SceneManager.LoadScene(0);
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LoadSave", 0);
    }

}
