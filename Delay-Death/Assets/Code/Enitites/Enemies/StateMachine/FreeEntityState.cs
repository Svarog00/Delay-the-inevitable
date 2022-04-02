namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public class FreeEntityState : IEntityState
    {
        private EntityStateMachine _stateMachine;
        private Enemy _agentContext;

        public FreeEntityState(EntityStateMachine enitityStateMachine, Enemy context)
        {
            _stateMachine = enitityStateMachine;
            _agentContext = context;
        }

        public void Handle()
        {
            
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
