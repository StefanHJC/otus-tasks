using UnityEngine;

namespace ShootEmUp
{
    public interface IUnitView : ISceneEntity, ISwitchable
    {
        IMoveComponent Movement { get; }
        IWeaponComponent Weapon { get; }
        IHitPointsComponent Health { get; }
    }

    public interface ISceneEntity
    {
        Vector2 Position { get; set; }
    }

    public interface ISwitchable
    {
        void Enable();
        void Disable();
    }

    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class UnitView : MonoBehaviour, IUnitView
    {
        [SerializeField] private MoveComponent _movement;
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private HitPointsComponent _hitPoints;

        public IMoveComponent Movement => _movement;
        public IWeaponComponent Weapon => _weapon;
        public IHitPointsComponent Health => _hitPoints;

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
    }
}