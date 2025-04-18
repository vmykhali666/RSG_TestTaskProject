using System;
using Content.Features.InteractionModule;
using UnityEngine;

namespace Content.Features.DamageablesModule.Scripts
{
    public class MonoDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;
        [SerializeField] private DamageableType _damageableType;
        [SerializeField] private AttackInteractable _attackInteractable;

        public Vector3 Position =>
            transform.position;

        public DamageableType DamageableType =>
            _damageableType;

        public bool IsActive =>
            _currentHealth > 0;

        public AttackInteractable Interactable =>
            _attackInteractable;

        public float MaxHealth =>
            _maxHealth;

        public float CurrentHealth =>
            _currentHealth;

        public event Action<float> OnDamaged;
        public event Action OnKilled;

        public event Action<float, float> OnHealthChanged;

        public void Damage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            OnDamaged?.Invoke(damage);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            if (_currentHealth > 0)
                return;
            Destroyed();
        }

        public void SetHealth(float health)
        {
            _currentHealth = health > _maxHealth ? _maxHealth : health;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        
        public void Destroyed()
        {
            OnKilled?.Invoke();
            Destroy(gameObject);
        }
    }
}