using UnityEngine;

namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public class EngageState : IEntityState
    {
        private EntityAttack _attack;

        private EntityStateMachine _stateMachine;
        private Entity _agentContext;

        public EngageState(Entity agentContext, EntityStateMachine stateMachine)
        {
            _agentContext = agentContext;
            _stateMachine = stateMachine;
            
            _attack = _agentContext.gameObject.GetComponentInChildren<EntityAttack>();
        }

        public void Enter()
        {
            
        }

        public void Handle()
        {
            if (_agentContext.GetDistanceToPlayer() > _attack.AttackDistanceGetter)
            {
                _stateMachine.Enter<ChaseState>();
            }
            else
            {
                _attack.DistanceToPlayer = _agentContext.GetDistanceToPlayer();
                _attack.Direction = _agentContext.GetDirectionToPlayer();
                _attack.Attack();
            }
        }

        public void Exit()
        {
            
        }
    }
}