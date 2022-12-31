using UnityEngine;

namespace DesignPatterns.EventMediator
{
    public abstract class Event
    {

    }

    public class AudioEvent : Event
    {
        public readonly AudioClip Clip;
        public readonly float Volume;

        public AudioEvent(AudioClip clip, float volume)
        {
            Clip = clip;
            Volume = volume;
        }
    }

    public class OnClickButtonEvent : Event
    {
        public readonly GameObject Button;

        public OnClickButtonEvent(GameObject button)
        {
            Button = button;
        }
    }
}