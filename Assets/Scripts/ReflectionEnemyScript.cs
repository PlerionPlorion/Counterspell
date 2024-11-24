using UnityEngine;

public class ReflectionEnemyBehavior : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private Transform player; // Reference to the player's position
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the horizontal direction toward the player
            float directionX = player.position.x - transform.position.x;

            // Normalize direction to 1 or -1
            directionX = Mathf.Sign(directionX);

            // Set the velocity, ensuring vertical velocity remains unchanged
            rb.velocity = new Vector2(directionX * speed, rb.velocity.y);
        }
    }
}
