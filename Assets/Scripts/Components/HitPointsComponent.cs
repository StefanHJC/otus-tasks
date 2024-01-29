using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IHitPointsComponent
    {
        event Action OnDeath;

        bool IsHitPointsExists();
        void TakeDamage(int damage);
    }

    public sealed class HitPointsComponent : MonoBehaviour, IHitPointsComponent
    {
        [SerializeField] private int _hitPoints;

        public event Action OnDeath;

        public bool IsHitPointsExists() => _hitPoints > 0;

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0) 
                OnDeath?.Invoke();
        }
    }
}