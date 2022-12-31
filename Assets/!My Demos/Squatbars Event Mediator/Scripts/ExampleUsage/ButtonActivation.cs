using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPatterns.EventMediator
{
    [RequireComponent(typeof(Collider))]
    public class ButtonActivation : MonoBehaviour
    {
        private Collider collider;

        void Start()
        {
            collider = GetComponent<Collider>();
        }

        public void OnClickButton()
        {
            new OnClickButtonEvent(this.gameObject).Fire();
        }

        void Update()
        {
            CheckCollider();
        }

        private void CheckCollider()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100f))
                {
                    if (hitInfo.collider == this.collider)
                    {
                        OnClickButton();
                    }
                }
            }
        }
    }
}
