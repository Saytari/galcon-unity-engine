using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Controllers.Enumerations;
using Models;

namespace Controllers
{
    public class DamageController : MonoBehaviour
    {
        // damage 
        public int damage = 1;

        // game manager
        Manager manager;

        void Awake()
        {
            // get manager component
            manager = GetComponent<Manager>();
        }

        public void handle(DamageEvent damageEvent)
        {
            // get event ship model
            ShipModel shipToDamageBy = damageEvent.getShip();

            // get event planet model
            PlanetModel planetToDamage = damageEvent.getPlanet();

            // get planet current counter number
            int planetCounter = planetToDamage.getShipsCount();

            // decrease counter by one 
            planetCounter -= damage;

            // set planet new counter number
            planetToDamage.setShipsCount(planetCounter);

            // if the planet gets zeroed replace the owner
            if(planetCounter <= 0)
            {
                // get the attacker player
                PlayerModel attacker = shipToDamageBy.getFlock().getOwner();

                // get the planet orginal owner if it exists
                PlayerModel oldOwner = planetToDamage.getOwner();
                
                // add planet to player conquered planets list
                attacker.addPlanet(planetToDamage);

                // if the planet has old owner
                if(oldOwner != null)
                    // remove planet from old owner conquered planets list
                    oldOwner.removePlanet(planetToDamage);

                // set the planet new owner
                planetToDamage.setOwner(attacker);
            }
            
            // dispatch delete event for the event ship
            manager.dispatch(new DeleteEvent(shipToDamageBy, DeleteMode.Spark));
        }

    }
}
