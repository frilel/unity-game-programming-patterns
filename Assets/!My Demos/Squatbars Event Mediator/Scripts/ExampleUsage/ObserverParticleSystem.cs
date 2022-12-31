using UnityEngine;

namespace DesignPatterns.EventMediator
{
    public class ObserverParticleSystem : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleSystem;

        private void Start()
        {
            EventMediator.Subscribe<OnClickButtonEvent>(OnThingHappened);
        }

        private void OnThingHappened(OnClickButtonEvent e)
        {
            if (particleSystem != null)
            {
                particleSystem.Stop();
                particleSystem.Play();
            }
        }

        private void OnDestroy()
        {
            // unsubscribe/deregister from the event if we destroy the object
            EventMediator.Unsubscribe<OnClickButtonEvent>(OnThingHappened);
        }

    }
}