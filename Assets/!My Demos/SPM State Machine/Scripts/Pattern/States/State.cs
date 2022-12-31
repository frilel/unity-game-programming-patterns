using UnityEngine;

namespace DesignPatterns.SPMState
{

    /// <summary>
    /// Base class for all states. This class is abstract and cannot be instantiated.
    /// Creating a new state is as simple as:
    /// create a new script that inherits from this class,
    /// don't forget to add the [CreateAssetMenu] attribute,
    /// and create a scriptable object asset for it in the Resource folder.
    /// </summary>
    public abstract class State : ScriptableObject
    {
        protected StateMachine StateMachine { get; private set; }
        protected PlayerController PlayerController { get; private set; }

        // color to change player
        // (alternately: set the color in the inspector in folder Resources/SPM StateMachine States)
        // each state can have its own color
        public Color MeshColor = Color.gray;

        public virtual void Initialize(StateMachine stateMachine)
        {
            this.StateMachine = stateMachine;
            this.PlayerController = Get<PlayerController>();
        }

        public abstract void Enter(); // code that runs when we first enter the state
        public abstract void Tick(); // per-frame logic, include condition to transition to a new state
        public abstract void Exit(); // code that runs when we exit the state

        public T TransitionTo<T>() where T : State => this.StateMachine.TransitionTo<T>();
        public T Get<T>() => this.StateMachine.Get<T>();
    }
}
