using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private MoveComponent _movement;
        [SerializeField] private WeaponComponent _weapon;

        public IMoveComponent Movement => _movement;
        public IWeaponComponent Weapon => _weapon;
    }
}