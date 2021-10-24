using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Controllers.Enumerations;
using Models;

namespace Controllers
{
    public class BoostController : MonoBehaviour
    {
        // boost 
        public int boost = 1;


        // game manager
        Manager manager;

        void Awake()
        {
            // get manager component
            manager = GetComponent<Manager>();
        }

        public void handle(BoostEvent boostEvent)
        {
            // get event ship model
            ShipModel shipToBoostBy = boostEvent.getShip();

            // get event planet model
            PlanetModel planetToBoost = boostEvent.getPlanet();

            // get planet current counter number
            int planetCounter = planetToBoost.getShipsCount();

            // increate counter by one 
            planetCounter += boost;

            // set planet new counter number
            planetToBoost.setShipsCount(planetCounter);

            // dispatch delete event for the event ship
            manager.dispatch(new DeleteEvent(shipToBoostBy, DeleteMode.Merge));

        }
    }
}
