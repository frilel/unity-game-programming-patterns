using System;
using System.Collections.Generic;

namespace DesignPatterns.EventMediator
{
    /// <summary>
    /// This class defines an event mediator, which is a utility for managing events in a C# program. 
    /// 
    /// It allows you to "subscribe" to events, which means that you can specify a piece of code (a delegate)
    /// that should be executed when the event occurs. It also allows you to "unsubscribe" from events,
    /// which means that you can remove your delegate from the list of those that should be executed when the event occurs. 
    /// Finally, it allows you to "fire" events, which means that it will execute all of the subscribed 
    /// delegates for a particular event.
    /// </summary>
    public static class EventMediator
    {
        static readonly Dictionary<Type, Delegate> globalListeners = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Adds a listener for the specified event type.
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="action">Action to perform on fired event</param>
        public static void Subscribe<T>(Action<T> action) => SubscribeInteral(globalListeners, action);
        public static void Unsubscribe<T>(Action<T> action) => UnsubscribeInternal(globalListeners, action);

        /// <summary>
        /// Fires an event of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public static void FireEvent<T>(T e) => FireEventInternal(globalListeners, e);
        public static void Fire<T>(this T e) => FireEventInternal(globalListeners, e);

        static void SubscribeInteral<T>(Dictionary<Type, Delegate> collection, Action<T> action)
        {
            if (!collection.ContainsKey(typeof(T)))
                collection.Add(typeof(T), action);
            else
                collection[typeof(T)] = Delegate.Combine(collection[typeof(T)], action);
        }
        static void UnsubscribeInternal<T>(Dictionary<Type, Delegate> collection, Action<T> action)
        {
            Delegate actions = null;

            if (collection.ContainsKey(typeof(T)))
                actions = Delegate.Remove(collection[typeof(T)], action);

            if (actions == null)
                collection.Remove(typeof(T));
            else
                collection[typeof(T)] = actions;
        }

        //TODO: DynamicInvoke not efficient. Look into alternatives if performance is an issue.
        //OLD COMMENT: Refactor to not use a boxing call, will prob. require some sort of structural refactor but should be fine
        static void FireEventInternal<T>(Dictionary<Type, Delegate> collection, T e)
        {
            if (collection.TryGetValue(typeof(T), out Delegate value))
                value.DynamicInvoke(e);
        }
    }
}