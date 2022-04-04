using UnityEngine;

namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public class DecayState : IEntityState
    {
        private Entity _agentContext;
        private EntityStateMachine _entityStateMachine;

        private float _decayTime;
        private float _curDecayTime;

        public DecayState(Entity context, EntityStateMachine stateMachine, float decayTime)
        {
            _entityStateMachine = stateMachine;
            _agentContext = context;
            _decayTime = decayTime;
        }

        public void Enter()
        {
            _curDecayTime = _decayTime;
        }

        public void Exit()
        {
            
        }

        public void Handle()
        {
            Decay();
        }

        private void Decay()
        {
            _curDecayTime -= Time.deltaTime;
            if (_curDecayTime <= 0)
            {
                _agentContext.gameObject.SetActive(false);
            }
        }
    }
}
