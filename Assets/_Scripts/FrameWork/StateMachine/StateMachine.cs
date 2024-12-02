using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.FSM
{
    public abstract class StateMachine
    {
        protected IState currentState;

        private Dictionary<string, IState> _stateTable = new();

        public void Initialize(Enum startState)
        {
            ChangeState(startState);
        }

        public void ChangeState(Enum newState)
        {
            var stateName = newState.ToString();
            if (_stateTable.TryGetValue(stateName, out IState state))
            {
                currentState?.Exit();
                currentState = state;
                currentState.Enter();
            }
        }
        
        public void LogicUpdate()
        {
            currentState?.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }

        public void RegisterState(Enum stateEnum, IState state)
        {
            _stateTable[stateEnum.ToString()] = state;
        }
    }
}
