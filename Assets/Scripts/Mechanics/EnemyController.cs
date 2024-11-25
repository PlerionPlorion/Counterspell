using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path and manages health.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;  // Patrol path for movement
        public AudioClip ouch;    // Sound for when enemy gets hurt

        internal PatrolPath.Mover mover;  // Movement logic for the enemy
        internal AnimationController control;  // Animation control
        internal Collider2D _collider;  // Enemy's collider
        internal AudioSource _audio;  // Audio source for the enemy
        SpriteRenderer spriteRenderer;  // Sprite renderer for flipping sprite direction
        //private Health enemyHealth;  // Reference to the enemy's health component

        public Bounds Bounds => _collider.bounds;  // Access the bounds of the enemy's collider

        void Awake()
        {
            // Get the necessary components
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            //enemyHealth = GetComponent<Health>();  // Get the Health component from the enemy

            // Ensure the Health component exists
          //  if (enemyHealth == null)
          //  {
           //     Debug.LogError("Enemy does not have a Health component.");
           // }
        }

        // Handle collision with the player
        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // Schedule the PlayerEnemyCollision event when the player collides with the enemy
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        void Update()
        {
            // Handle patrol movement if a patrol path is assigned
            if (path != null)
            {
                if (mover == null) 
                    mover = path.CreateMover(control.maxSpeed * 0.5f);  // Create mover if not already assigned
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);  // Move the enemy
            }
        }

        // Method to handle damage when the enemy collides with the player
       
    }
}
