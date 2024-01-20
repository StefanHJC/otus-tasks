using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class AttackInputListener : ITickable, IAttackInput
    {
        private readonly KeyCode _attackKey;
        
        public event Action AttackActionPerformed;

        public AttackInputListener(KeyCode attackKey)
        {
            _attackKey = attackKey;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(_attackKey))
                AttackActionPerformed?.Invoke();
        }
    }
}