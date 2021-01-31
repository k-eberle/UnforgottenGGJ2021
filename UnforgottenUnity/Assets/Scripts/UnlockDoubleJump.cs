using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoubleJump : MonoBehaviour
{
    public string description = "You unlocked the DoubleJump!\nPress Space in midair to Jump again";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().unlockedDoubleJump = true;
            GameObject.FindObjectOfType<LevelManager>().ShowCustomTextNext(description);
        }
    }
}
