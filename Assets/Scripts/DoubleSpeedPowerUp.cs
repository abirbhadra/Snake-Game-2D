using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedPowerUp : MonoBehaviour
{
    public Collider2D gridArea;
    public float respawnTime = 20f; // Adjust this value to set the respawn time

    private Snake snake;

    private void Awake()
    {
        snake = FindObjectOfType<Snake>();
        StartCoroutine(RespawnFoodRoutine());
    }

    private void Start()
    {
        RandomizePosition();
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
            StartCoroutine(RespawnFood());
        }

        else if (other.gameObject.CompareTag("Player1"))
        {
            StartCoroutine(RespawnFood());
        }
    }

    private IEnumerator RespawnFood()
    {
        // Disable the collider and renderer while waiting for respawn
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        // Wait for the specified respawn time
        yield return new WaitForSeconds(respawnTime);

        // Enable the collider and renderer for the food to respawn
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Renderer>().enabled = true;

        // Randomize the position of the food
        RandomizePosition();
    }
    private IEnumerator RespawnFoodRoutine()
    {
        while (true)
        {
            // Respawn food immediately on awake, then wait for 10 seconds
            RandomizePosition();
            yield return new WaitForSeconds(15f);
        }
    }
}

