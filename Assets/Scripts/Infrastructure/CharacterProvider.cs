using System;

namespace ShootEmUp
{
    public sealed class CharacterProvider : IDisposable
    {
        private CharacterController _character;

        public event Action CharacterDied;

        public CharacterController Character
        {
            get => _character;
            set
            {
                _character = value;
                _character.View.GetComponent<HitPointsComponent>().DeathHappened += InvokeDeathEvent;
            }
        }

        public void Dispose() => _character.View.GetComponent<HitPointsComponent>().DeathHappened -= InvokeDeathEvent;

        private void InvokeDeathEvent() => CharacterDied?.Invoke();
    }
}