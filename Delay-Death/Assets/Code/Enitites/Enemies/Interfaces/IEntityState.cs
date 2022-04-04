
namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public interface IEntityState
    {
        public void Handle();
        public void Enter();
        public void Exit();
    }
}
