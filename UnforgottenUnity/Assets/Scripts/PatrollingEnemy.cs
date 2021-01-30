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


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentRepetition = 0;
        moveLeft = startLeft;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Test");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit!");
        }
    }
}