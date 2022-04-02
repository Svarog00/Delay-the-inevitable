using System;
using UnityEngine;

namespace Assets.Code.Enitites.Enemies
{
    public class EnemyHealth : IHealth
    {
        public event EventHandler OnDieEventHandler;

        private int _healthPoints;
        private int _currentHealth;

        public EnemyHealth(int initialHealthPoints)
        {
            _healthPoints = initialHealthPoints;
            _currentHealth = _healthPoints;
        }

        public void Hurt(int damage)
        {
            _currentHealth -= damage;
            if( _currentHealth < 0 )
            {
                Die();
            }
        }

        public void Die()
        {
            OnDieEventHandler?.Invoke(this, EventArgs.Empty);
        }

        public void Reset()
        {
            _currentHealth = _healthPoints;
        }

    }
}
