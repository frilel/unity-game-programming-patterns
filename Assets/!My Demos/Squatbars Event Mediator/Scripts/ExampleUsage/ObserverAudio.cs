using DesignPatterns.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.EventMediator
{
    [RequireComponent(typeof(AudioSource))]
    public class ObserverAudio : MonoBehaviour
    {
        [SerializeField] float delay = 0f;
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            EventMediator.Subscribe<OnClickButtonEvent>(OnThingHappened);
        }

        public void OnThingHappened(OnClickButtonEvent e)
        {
            StartCoroutine(PlayWithDelay());
        }

        IEnumerator PlayWithDelay()
        {
            yield return new WaitForSeconds(delay);
            source.Stop();
            source.Play();
        }

        private void OnDestroy()
        {
            // unsubscribe/deregister from the event if we destroy the object
            EventMediator.Unsubscribe<OnClickButtonEvent>(OnThingHappened);
        }
    }
}
