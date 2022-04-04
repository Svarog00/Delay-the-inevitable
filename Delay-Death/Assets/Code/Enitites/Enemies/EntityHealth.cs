using System;

namespace Assets.Code.Enitites.Enemies
{
    public class EntityHealth : IHealth
    {
        public event EventHandler OnDieEventHandler;

        private int _healthPoints;
        private int _currentHealth;

        public EntityHealth(int initialHealthPoints)
        {
            _healthPoints = initialHealthPoints;
            _currentHealth = _healthPoints;
        }

        public void Hurt(int damage)
        {
            if(_currentHealth > 0)
            {
                _currentHealth -= damage;
            }
            else
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
