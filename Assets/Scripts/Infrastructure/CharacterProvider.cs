using System;
using ShootEmUp;

public sealed class CharacterProvider
{
    private CharacterController _character;

    public CharacterController Character
    {
        get => _character;
        set
        {
            _character = value;
            _character.View.GetComponent<HitPointsComponent>().DeathHappened += CharacterDied;
        }
    }

    public event Action CharacterDied;
}