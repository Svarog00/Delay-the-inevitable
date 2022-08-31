using System;

namespace Assets.Code.Enitites.Enemies
{
    public class EntityHealth : IHealth
    {
        public int CurHealth => _currentHealth;

        public event EventHandler OnDieEventHandler;

        private int _maxHealthPoints;
        private int _currentHealth;

        public EntityHealth(int initialHealthPoints)
        {
            _maxHealthPoints = initialHealthPoints;
            _currentHealth = _maxHealthPoints;
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
            _currentHealth = _maxHealthPoints;
        }

    }
}
