using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private MoveComponent _movement;
        [SerializeField] private WeaponComponent _weapon;

        public MoveComponent Movement => _movement;
        public WeaponComponent Weapon => _weapon;
    }
}