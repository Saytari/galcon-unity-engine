using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Controllers.Enumerations;
using Controllers.Contracts;
using Controllers.Events;
using Models.Enumerations;
using Models.Contracts;
using Views.Contracts;

namespace Views
{
    public class PlanetView : MonoBehaviour, IPlanetView
    {
        // text component
        protected Text text;

        // sprite renderer component
        protected SpriteRenderer renderer;

        // animator component
        protected Animator animator;

        // get smoke image component
        protected Image smoke;

        // planet planetController contract
        protected IPlanetController planetController;

        // planet planetModel contract
        protected IPlanetModel planetModel;

        void Awake()
        {
            // get the planet planetController component attached to this planet instance
            planetController = GetComponent<IPlanetController>();

            // get the planet planetModel component attached to this planet instance
            planetModel = GetComponent<IPlanetModel>();

            // get text component attached to this instace
            text = GetComponentInChildren<Text>();

            // get text component attached to this instace
            smoke = GetComponentInChildren<Image>();

            // get sprite renderer component 
            renderer = GetComponent<SpriteRenderer>();

            // get animator componenet
            animator = GetComponent<Animator>();
        }

        void OnMouseUpAsButton()
        {   
            // detect select mode
            SelectMode mode = Input.GetKey("left ctrl") ? SelectMode.Include : SelectMode.Action;
            // create select event
            SelectEvent selectEvent = new SelectEvent(planetModel, mode);
            // dispatch event to local controller
            planetController.dispatch(selectEvent);
        }

        // interface implementation
        public void refreshCount(int count)
        {
            // tell text component to rendere new count
            text.text = count + "";
        }
        
        // interface implementation
        public void refreshSelection(bool selectState)
        {
            // set or not the flicking animation
            animator.SetBool("Flicking", selectState);
        }

        // interface implementation
        public void refreshType(PlanetType newType)
        {
            // change planet sprite or image
            renderer.sprite = Resources.Load<Sprite>("Sprites/Planets/" + newType);

            // generate smoke object
            generateSmoke(newType);
        }

        void generateSmoke(PlanetType type)
        {
            UnityEngine.Object prefab = Resources.Load("Prefabs/Animations/Smoke");

            GameObject generatedSmoke = Instantiate(prefab, transform) as GameObject;

            generatedSmoke.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Animations/"+ type + "Smoke");

            generatedSmoke.transform.SetParent(transform);
            
        }

    }
}
