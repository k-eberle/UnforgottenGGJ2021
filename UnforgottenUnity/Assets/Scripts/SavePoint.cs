using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("LoadSave", 1);
            PlayerPrefs.SetFloat("SaveX", transform.position.x);
            PlayerPrefs.SetFloat("SaveY", transform.position.y);
            PlayerPrefs.SetFloat("SaveZ", transform.position.z);
        }

    }
}
