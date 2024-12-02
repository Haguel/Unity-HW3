using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100f;
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            print("Enemy health: " + _currentHealth);
        }
    }
}