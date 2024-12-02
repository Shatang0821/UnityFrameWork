using UnityEngine;

namespace Framework.FSM
{
    public class AnimationState : BaseState
    {
        // データクラスの変数
        //...

        protected int stateHash;                //アニメーションのハッシュ値
        
        protected Animator animator;            //アニメーターコンポーネント

        // アニメーション遷移時間
        protected static float transitionDuration;

        public AnimationState(string animName, StateMachine stateMachine,Animator animator) : base(stateMachine)
        {
            this.animator = animator;
            stateHash = Animator.StringToHash(animName);
            transitionDuration = 0;
        }

        public override void Enter()
        {
            base.Enter();
            animator.CrossFade(stateHash,transitionDuration);
        }
        
        //ステート変更
        protected override void ChangeState<TEnum>(TEnum state)
        {
            //transitionDuration = stateConfig.GetTransitionDuration(state.ToString());
            base.ChangeState(state);
        }
    }
}