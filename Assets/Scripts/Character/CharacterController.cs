
namespace ShootEmUp
{
    public sealed class CharacterController : IService
    {
        public UnitView View { get; private set; }

        public CharacterController(UnitView view)
        {
            View = view;
        }
    }
}