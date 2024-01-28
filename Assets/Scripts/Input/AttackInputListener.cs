using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class AttackInputListener : ITickable, IAttackInput
    {
        private readonly KeyCode _attackKey;
        
        public event Action AttackActionPerformed;

        [Inject]
        public AttackInputListener(IDatabaseService data)
        {
            _attackKey = data.Get<GameStaticData>().FirstOrDefault().InputSchema.AttackKey;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(_attackKey))
                AttackActionPerformed?.Invoke();
        }
    }
}