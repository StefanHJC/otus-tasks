using System;

namespace ShootEmUp
{
    public interface IAttackInput
    {
        event Action AttackActionPerformed;
    }
}