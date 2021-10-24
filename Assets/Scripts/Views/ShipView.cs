using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Contracts;
using Controllers.Events;
using Controllers.Enumerations;
using Models;
using Models.Contracts;
using Views.Contracts;

namespace Views
{
    public class ShipView : MonoBehaviour, IShipView
    {
        // animator component
        protected Animator animator;

        // planet ship controller contract
        protected IShipController shipController;

        // planet ship model contract
        protected IShipModel shipModel;

        // get components attached to this ship instance
        void Awake()
        {
            // get the ship controller component
            shipController = GetComponent<IShipController>();

            // get the ship ship model component
            shipModel = GetComponent<IShipModel>();

            // get animator componenet
            animator = GetComponent<Animator>();
        }


        void OnCollisionStay2D(Collision2D collision)
        {   
            // get collided object model
            Model collideModel = collision.gameObject.GetComponent<Model>();
            // dispatch collision event to local controller
            shipController.dispatch(new CollisionEvent(shipModel, collideModel));
        }
        
        void OnMouseDown()
        {   
            // detect select mode
            SelectMode mode = Input.GetKey("left ctrl") ? SelectMode.Include : SelectMode.Action;
            // create select event
            SelectEvent selectEvent = new SelectEvent(shipModel, mode);
            // dispatch select event to local controller
            shipController.dispatch(selectEvent);
        }

        // interface implementation
        public void refreshSelection(bool selectState)
        {
            // loop animation
            animator.SetBool("Flicking", selectState);
        }

        // interface implementation
        public void fadeOut(DeleteMode mode)
        {
            // delete collider
            Destroy(GetComponent<Collider2D>());
            // game object gets deleted after animation ends
            animator.SetBool("FadeOut", true);
        }


        // animation event function
        public void destroy()
        {
            Destroy(gameObject);
        }
    }
}
