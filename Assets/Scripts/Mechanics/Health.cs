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

        public void Increment()
        {
            currentHP = currentHP + 1;
        }

        public void HPOnRespawn()
        {
            currentHP = 4;
        }

        public void Decrement()
        {
            currentHP = currentHP - 1;
            // Trigger necessary actions when health reaches zero
            Debug.Log($"Player Health: {currentHP}/3");
            if (currentHP == 0)
    {
        Debug.Log("Health is zero. Scheduling PlayerDeath.");
        Schedule<PlayerDeath>();
    }
        }

        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
