using System;

namespace Framework.FSM
{
    public class BaseState : IState
    {
        protected float stateTimer = 0;  // ステート持続時間
        protected StateMachine stateMachine;    //状態マシンインスタンス

        public BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public virtual void Enter()
        {
            stateTimer = 0.0f;
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            stateTimer += UnityEngine.Time.deltaTime;
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        protected virtual void ChangeState<TEnum>(TEnum state)where TEnum : Enum
        {
            stateMachine.ChangeState(state);
        }
    }
}