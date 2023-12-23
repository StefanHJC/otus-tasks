using UnityEngine;

namespace ShootEmUp
{
    public interface IUnitView
    {
        IMoveComponent Movement { get; }
        IWeaponComponent Weapon { get; }
        Vector2 Position { get; }
    }

    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class UnitView : MonoBehaviour, IUnitView
    {
        [SerializeField] private MoveComponent _movement;
        [SerializeField] private WeaponComponent _weapon;

        public IMoveComponent Movement => _movement;
        public IWeaponComponent Weapon => _weapon;
        public Vector2 Position => transform.position;
    }
}