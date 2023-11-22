using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "InputSchema", menuName = "Input/New Schema")]
    public class InputSchema : ScriptableObject
    {
        public KeyCode AttackKey = KeyCode.Space;
        public KeyCode MoveLeftKey = KeyCode.LeftArrow;
        public KeyCode MoveRightKey = KeyCode.RightArrow;
    }
}