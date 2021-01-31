using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDashAndDoubleJump : MonoBehaviour
{
    public string description = "You unlocked Dash and DoubleJump!\nPress Space in midair to Jump again\nPress Rightclick or Ctrl to Dash";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().unlockedDash = true;
            collision.gameObject.GetComponent<PlayerController>().unlockedDoubleJump = true;
            GameObject.FindObjectOfType<LevelManager>().ShowCustomTextNext(description);
        }
    }
}
