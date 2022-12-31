using UnityEngine;

namespace DesignPatterns.EventMediator
{
    public class Subject : MonoBehaviour
    {

        private AudioClip clip;
        private float volume;

        private AudioSource audioSource;

        void Start()
        {
            EventMediator.Subscribe<AudioEvent>(OnEventFired);
        }

        void OnEventFired(AudioEvent e)
        {
            audioSource.clip = e.Clip;
            audioSource.volume = e.Volume;
            audioSource.Stop();
            audioSource.Play();
        }

        void ExampleMethodThatFiresEvent()
        {
            new AudioEvent(clip, volume).Fire();
            //EventMediator.FireEvent(new AudioEvent(clip, volume)); // same as above
        }

        private void OnDestroy()
        {
            EventMediator.Unsubscribe<AudioEvent>(OnEventFired);
        }
    }
}