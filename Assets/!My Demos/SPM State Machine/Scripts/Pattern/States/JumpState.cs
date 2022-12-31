using UnityEngine;

namespace DesignPatterns.SPMState
{

    [CreateAssetMenu(menuName = "States/Player/Jump")]
    public class JumpState : State
    {
        public override void Enter()
        {
        }
        public override void Tick()
        {
            if (PlayerController.IsGrounded)
            {
                if (Mathf.Abs(PlayerController.CharController.velocity.x) > 0.1f || Mathf.Abs(PlayerController.CharController.velocity.z) > 0.1f)
                {
                    base.TransitionTo<IdleState>();
                }
                else
                {
                    base.TransitionTo<WalkState>();
                }
            }
        }
        public override void Exit()
        {

        }
    }
}