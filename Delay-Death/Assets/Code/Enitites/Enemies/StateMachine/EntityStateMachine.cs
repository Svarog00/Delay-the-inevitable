using System;
using System.Collections.Generic;

namespace Assets.Code.Enitites.Enemies.StateMachine
{
    public class EntityStateMachine
    {
        private IEntityState _currentState;
        private Dictionary<Type, IEntityState> _states;

        public IEntityState CurrentState
        {
            get => _currentState;
        }

        public Dictionary<Type, IEntityState> States
        {
            get => _states;
            set => _states = value;
        }


        public void Work()
        {
            _currentState.Handle();
        }

        public void Enter<TState>() where TState : class, IEntityState
        {
            _currentState = ChangeState<TState>();
            _currentState.Enter();
        }

        public TState ChangeState<TState>() where TState : class, IEntityState
        {
            _currentState?.Exit();
            return _states[typeof(TState)] as TState;
        }
    }
}
