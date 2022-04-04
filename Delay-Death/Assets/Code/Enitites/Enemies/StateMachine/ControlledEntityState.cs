using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public class ControlledEntityState : IEntityState
    {
        private const string PlayerTag = "Player";
        private const string EnemyTag = "Enemy";

        private EntityStateMachine _stateMachine;

        private IInputService _inputInputService;

        private Entity _agentContext;
        private EnemyMovement _enemyMovement;
        private EntityAttack _attackScript;

        public ControlledEntityState(EntityStateMachine entityStateMachine, Entity context, IInputService inputService)
        {
            _stateMachine = entityStateMachine;
            _inputInputService = inputService;
            _agentContext = context;

            _enemyMovement = _agentContext.GetComponent<EnemyMovement>();
            _attackScript = _agentContext.GetComponent<EntityAttack>();
        }

        public void Handle()
        {
            Control();
        }

        private void Control()
        {
            if (_inputInputService.Axis.sqrMagnitude > 0)
            {
                _enemyMovement.HandleMove(_inputInputService.Axis);
            }
            else
            {
                _enemyMovement.Stop();
            }

            if(_inputInputService.IsMeleeAttackButtonDown())
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _attackScript.Direction = (mousePosition - _agentContext.transform.position).normalized;
                _attackScript.Attack();
            }
        }

        public void Enter()
        {
            _agentContext.gameObject.tag = PlayerTag;
        }

        public void Exit()
        {
            _agentContext.tag = EnemyTag;
        }

    }
}
