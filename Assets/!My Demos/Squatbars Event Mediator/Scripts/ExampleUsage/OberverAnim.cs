using UnityEngine;

namespace DesignPatterns.EventMediator
{
    public class ObserverAnim : MonoBehaviour
    {
        [SerializeField] Animation animClip;

        void Start()
        {
            EventMediator.Subscribe<OnClickButtonEvent>(OnThingHappened);
        }

        private void OnThingHappened(OnClickButtonEvent e)
        {
            if (animClip != null)
            {
                animClip.Stop();
                animClip.Play();
            }
        }

        private void OnDestroy()
        {
            EventMediator.Unsubscribe<OnClickButtonEvent>(OnThingHappened);
        }
    }
}
