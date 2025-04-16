using System;
using Content.Features.AIModule.Scripts;
using Content.Features.InteractionModule;
using UnityEngine;

namespace Content.Features.DamageablesModule.Scripts
{
    public class MonoDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _currentHealth = 100f;
        [SerializeField] private float _maxHealth = 100f;
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

        public event Action<float> OnDamaged;
        public event Action OnKilled;

        public event Action<float, float> OnHealthChanged;

        public void Damage(float damage)
        {
            _currentHealth -= damage;
            OnDamaged?.Invoke(damage);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth > 0)
                return;

            OnKilled?.Invoke();
            Destroy(gameObject);
        }

        public void SetHealth(float health) =>
            _maxHealth = health;
    }
}