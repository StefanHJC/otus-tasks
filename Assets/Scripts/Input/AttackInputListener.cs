using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class AttackInputListener : IService, IUpdateListener
    {
        private readonly KeyCode _attackKey;
        
        public Action AttackActionPerformed;

        public AttackInputListener(KeyCode attackKey)
        {
            _attackKey = attackKey;
        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(_attackKey))
                AttackActionPerformed?.Invoke();
        }
    }
}