using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "UIData", menuName = "Static data/New UI static data")]
    public class UIStaticData : ScriptableObject
    {
        public HUD Hud;
    }
}