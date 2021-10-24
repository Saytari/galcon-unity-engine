using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Contracts;
using Controllers.Events;
using Models;
using Models.Enumerations;
using Views;

namespace Controllers
{
    public class PlanetController : MonoBehaviour, IPlanetController
    {
        // ship creating time
        public float averageTime = 1f;

        // next creation time
        protected float nextCreation;

        // planet planetModel
        protected PlanetModel planetModel;

        // game manager
        Manager manager;

        // get components attached to this object
        void Awake()
        {
            // get the planet planetModel component
            planetModel = GetComponent<PlanetModel>();
            
            // get game manager object
            manager = FindObjectOfType<Manager>();

            // set next creation time
            nextCreation = Time.time + averageTime;
        }

        // pass parameters to other components
        void Start()
        {
            // set random ships count
            planetModel.setShipsCount(Random.Range(0, planetModel.getCapacity()));
        }

        // Update is called once per frame
        void Update()
        {
            if(planetModel.getType() != PlanetType.GrayWhale)
                // produce new ship if possible
                produce();
        }

        void produce()
        {

            // if next ship creation time has come
            if(Time.time >= nextCreation)
            {
                // increase current ships count by one
                planetModel.setShipsCount(planetModel.getShipsCount() + 1);
                // calculate next ship creation time
                nextCreation = Time.time + (Random.Range(0, averageTime * 2f));
            }
        }

        // interface implementation
        public void dispatch(InputEvent inputEvent)
        {
            // inform manager about the event
            manager.dispatch(inputEvent);
        }

    }
}
