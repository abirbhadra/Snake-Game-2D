﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Food : MonoBehaviour
{
    public Collider2D gridArea;
    private Snake snake;
    

    private void Awake()
    {
        snake = FindObjectOfType<Snake>();

        
        StartCoroutine(RespawnFoodRoutine());
    }

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        while (snake.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;

                if (y > bounds.max.y)
                {
                    y = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RandomizePosition();
        }

        else if (other.gameObject.CompareTag("Player1"))
        {
            RandomizePosition();
        }
       
    }

    private IEnumerator RespawnFoodRoutine()
    {
        while (true)
        {
            // Respawn food immediately on awake, then wait for 10 seconds
            RandomizePosition();
            yield return new WaitForSeconds(20f);
        }
    }
    
}