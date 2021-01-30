﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PatrollingEnemy : MonoBehaviour
{
    public float speed;
    public int repetitions;
    public bool startLeft;

    private SpriteRenderer spriteRenderer;
    private int currentRepetition;
    private bool moveLeft;

    private Animator animator;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentRepetition = 0;
        moveLeft = startLeft;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 direction;

        if (moveLeft)
            direction = Vector2.left;
        else
            direction = Vector2.right;

        transform.position = transform.position + speed * Time.deltaTime * direction;
        transform.localScale = new Vector3(moveLeft ? 1 : -1, 1, 1);
    }

    public void AnimationEnd()
    {
        currentRepetition++;

        if (currentRepetition >= repetitions)
        {
            currentRepetition = 0;
            moveLeft = !moveLeft;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            Destroy(this.gameObject,.1f);
            this.animator.SetBool("isDying", true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Test");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage();
            Debug.Log("Hit!");
        }
    }
}
