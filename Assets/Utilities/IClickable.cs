using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    abstract class IClickable : MonoBehaviour
    {
        void Update()
        {
            AltUpdate();
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                RaycastHit raycast;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Physics.Raycast(ray, out raycast, 1000f);
                if (raycast.collider == gameObject.GetComponent<Collider2D>()) {/* Click();*/ }
            }
        }

        void OnMouseDown()
        {
            Click();
        }

        public abstract void Click();
        public virtual void AltUpdate() { }
        public virtual void AltOnMouseDown() { }
    }
}
