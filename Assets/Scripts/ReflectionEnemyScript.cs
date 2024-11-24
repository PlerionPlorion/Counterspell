using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A standalone controller for the reflection enemy.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ReflectionEnemyController : MonoBehaviour
    {
        public float followSpeed = 2f; // Speed at which the reflection enemy follows the player
        public float stopDistance = 1f; // Distance at which the reflection enemy stops moving
        public float lifespan = 10f; // Time before the reflection enemy despawns

        private Transform player;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;
        private float spawnTime;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
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

            // Record the spawn time for lifespan management
            spawnTime = Time.time;
        }

        void Update()
        {
            // Despawn the enemy after its lifespan
            if (Time.time - spawnTime > lifespan)
            {
                Destroy(gameObject);
                return;
            }

            if (player != null)
            {
                // Calculate direction to the player
                Vector2 direction = (player.position - transform.position);

                // Stop moving if within the stop distance
                if (direction.magnitude > stopDistance)
                {
                    direction.Normalize();
                    rb.velocity = new Vector2(direction.x * followSpeed, rb.velocity.y);

                    // Flip sprite based on direction
                    spriteRenderer.flipX = direction.x < 0;
                }
                else
                {
                    // Stop movement
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Trigger a collision event between the reflection enemy and the player
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = playerController;
                ev.enemy = null; // No need to assign EnemyController since this is a standalone script
            }
        }
    }
}
