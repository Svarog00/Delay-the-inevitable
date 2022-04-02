using UnityEngine;

namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public class ChaseState : IEntityState
	{
        private EnemyMovement _movement;
		private EntityAttack _attackScript;

        private EntityStateMachine _stateMachine;
		private Enemy _agentContext;

        private Vector2 _direction;

        public ChaseState(Enemy agentContext, EntityStateMachine stateMachine)
        {
			_agentContext = agentContext;
			_stateMachine = stateMachine;

			_movement = _agentContext.gameObject.GetComponent<EnemyMovement>();
			_attackScript = _agentContext.gameObject.GetComponentInChildren<EntityAttack>();
        }

		public void Enter()
		{

		}

		public void Handle()
		{
			_direction = _agentContext.GetDirectionToPlayer();
			_movement.HandleMove(_direction);

			if (_agentContext.DistanceToPlayer <= _attackScript.AttackDistanceGetter) //Если игрок слишком близко, то остановиться для атаки
			{
				_movement.Stop();
				_stateMachine.Enter<EngageState>();
			}
		}

        public void Exit()
        {
            
        }
    }
}