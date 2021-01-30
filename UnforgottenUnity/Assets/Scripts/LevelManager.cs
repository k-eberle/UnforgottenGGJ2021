using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int nextScene;

    private Canvas canvas;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
        //text = canvas.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNextText()
    {
        //canvas.enabled = true;

    }


}
