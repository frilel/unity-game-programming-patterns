using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DesignPatterns.SPMState
{
    public class StateMachine
    {
        public State Current { get; private set; }
        public State Next => next;
        State next;

        // event to notify other objects of the state change
        public event Action<StateMachine> StateChanged;

        // dictionary of all states in the state machine
        public readonly Dictionary<Type, State> States = new Dictionary<Type, State>();

        // pushing states onto a stack in case we want a "TransitionBack" functionality
        // (not implemented in this example)
        readonly Stack<State> queue = new Stack<State>();

        // dictionary with objects that can be accessed by the state machine
        Dictionary<Type, object> context;

        public void Initialize(State[] states, Type defaultState, Dictionary<Type, object> context)
        {
            this.context = context;

            // instantiate all states and initialize them
            for (int i = 0; i < states.Length; i++)
            {
                this.States.Add(states[i].GetType(), UnityEngine.Object.Instantiate(states[i]));
                this.States[states[i].GetType()].Initialize(this);
            }

            this.Current = this.States[defaultState];
            this.Current.Enter();

            // notify other objects that state has changed
            StateChanged?.Invoke(this);
        }

        public void Tick()
        {
            this.Current.Tick();

            UpdateCurrentState();
        }
        void UpdateCurrentState()
        {
            // if we have a state as NEXT, transition to it
            if (this.Next != null && this.Next != this.Current)
            {
                this.Current?.Exit();

                // save the state we're transitioning from
                if (this.Current != null)
                    queue.Push(this.Current);

                this.Current = Next;
                this.Current.Enter();

                // notify other objects that state has changed
                StateChanged?.Invoke(this);
            }
        }

        public T TransitionTo<T>() where T : State
        {
            if (!States.TryGetValue(typeof(T), out next))
                next = States.Values.OfType<T>().First();

            return (T)next;
        }

        /// <summary>
        /// get an object from the stored context, preferably cache this and don't use in Update
        /// </summary>
        public T Get<T>() => (T)context[typeof(T)];

        /// <summary>
        /// change one of the objects of a type in the context
        /// </summary>
        public void Set<T>(T value)
        {
            if (!context.TryGetValue(typeof(T), out _))
                context.Add(typeof(T), value);
            else
                context[typeof(T)] = value;
        }
    }
}
