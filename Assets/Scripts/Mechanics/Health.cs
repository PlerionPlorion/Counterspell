using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Health : MonoBehaviour
    {
        public int maxHP = 3;
        private int currentHP;

        public int CurrentHP => currentHP;  // Expose current HP for easy access

        public bool IsAlive => currentHP > 0;

        private GameObject heart1;
        private GameObject heart2;
        private GameObject heart3;

        // Increment health and add hearts
        public void Increment()
        {
            if (currentHP >= 4)
            {

            }
            if (currentHP < maxHP)
            {
                currentHP = currentHP + 1;

                // Add hearts on increment (if not already at maxHP)
                if (currentHP == 2)
                {
                    heart2.SetActive(true); // Enable heart2
                    heart1.SetActive(true);
                }
                else if (currentHP == 3)
                {
                    heart3.SetActive(true); // Enable heart3
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                }
            }

            Debug.Log($"Player Health: {currentHP}/{maxHP}");
        }

        // Reset health to max and respawn hearts
        public void HPOnRespawn()
        {
            currentHP = maxHP;
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }

        // Decrement health and destroy hearts
        public void Decrement()
        {
            if (currentHP > 0)
            {
                currentHP = currentHP - 1;

                if (currentHP == 2)
                {
                    heart3.SetActive(false); // Disable heart3
                    heart2.SetActive(true);
                    heart1.SetActive(true);
                }
                else if (currentHP == 1)
                {
                    heart2.SetActive(false); // Disable heart2
                    heart1.SetActive(true);
                }
                else if (currentHP == 0)
                {
                    heart1.SetActive(false); // Disable heart1
                }

                Debug.Log($"Player Health: {currentHP}/3");

                // Trigger necessary actions when health reaches zero
                if (currentHP == 0)
                {
                    Debug.Log("Health is zero. Scheduling PlayerDeath.");
                    Schedule<PlayerDeath>();
                }
            }
        }

        // Die method, decrementing health to zero
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = 1;  // Start with 1 heart
            heart1 = GameObject.Find("Heart_1");
            heart2 = GameObject.Find("Heart_2");
            heart3 = GameObject.Find("Heart_3");

            // Start with only the first heart active
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
    }
}
