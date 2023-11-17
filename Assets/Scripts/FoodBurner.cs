using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class FoodBurner : MonoBehaviour
{
    public Collider2D gridArea;
    private Snake snake;
    public float respawnTime = 20f;

    private void Awake()
    {
        snake = FindObjectOfType<Snake>();

        // Start the coroutine to respawn food immediately and then every 10 seconds
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
            StartCoroutine(RespawnFood());
        }

        else if (other.gameObject.CompareTag("Player1"))
        {
            StartCoroutine(RespawnFood());
        }
        RandomizePosition();
    }

    private IEnumerator RespawnFoodRoutine()
    {
        while (true)
        {
            // Respawn food immediately on awake, then wait for 10 seconds
            RandomizePosition();
            yield return new WaitForSeconds(10f);
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
}
