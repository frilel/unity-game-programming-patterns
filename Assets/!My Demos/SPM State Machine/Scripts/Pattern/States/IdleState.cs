using UnityEngine;

namespace DesignPatterns.SPMState
{
    [CreateAssetMenu(menuName = "States/Player/Idle")]
    public class IdleState : State
    {

        public override void Enter()
        {
        }
        public override void Tick()
        {
            // if we're no longer grounded, transition to jumping
            if (!PlayerController.IsGrounded)
            {
                base.TransitionTo<JumpState>();
            }

            // if we move above a minimum threshold, transition to walking
            if (Mathf.Abs(PlayerController.CharController.velocity.x) > 0.1f || Mathf.Abs(PlayerController.CharController.velocity.z) > 0.1f)
            {
                base.TransitionTo<WalkState>();
            }
            
        }
        public override void Exit()
        {

        }
    }
}
