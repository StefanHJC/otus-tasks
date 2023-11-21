using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class AttackInputListener
    {
        private readonly KeyCode _attackKey;
        
        public Action AttackActionPerformed;

        public AttackInputListener(KeyCode attackKey)
        {
            _attackKey = attackKey;
        }

        public void Update()
        {
            if (Input.GetKeyDown(_attackKey)) 
                AttackActionPerformed?.Invoke();
        }
    }
}