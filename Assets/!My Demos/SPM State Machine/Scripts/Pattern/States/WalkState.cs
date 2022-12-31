using UnityEngine;

namespace DesignPatterns.SPMState
{

    [CreateAssetMenu(menuName = "States/Player/Walk")]
    public class WalkState : State
    {
        public override void Enter()
        {
        }
        public override void Tick()
        {
            // if we are no longer grounded, transition to jumping
            if (!PlayerController.IsGrounded)
            {
                base.TransitionTo<JumpState>();
            }

            // if we slow to within a minimum velocity, transition to idling/standing
            if (Mathf.Abs(PlayerController.CharController.velocity.x) < 0.1f && Mathf.Abs(PlayerController.CharController.velocity.z) < 0.1f)
            {
                base.TransitionTo<IdleState>();
            }
            
        }
        public override void Exit()
        {

        }
    }
}