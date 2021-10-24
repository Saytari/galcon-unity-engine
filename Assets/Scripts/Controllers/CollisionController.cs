using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;

namespace Controllers
{
    public class CollisionController : MonoBehaviour
    {
        
        // game manager
        Manager manager;

        void Awake()
        {
            // get manager component
            manager = GetComponent<Manager>();
        }

        public void handle(CollisionEvent collisionEvent)
        {
            // get event dispatcher ship model
            ShipModel dispatcherShip = collisionEvent.getModel() as ShipModel;

            // route events for the correct process and casts models
            if(collisionEvent.getOtherModel() is ShipModel)
                process(dispatcherShip, collisionEvent.getOtherModel() as ShipModel);
            else if(collisionEvent.getOtherModel() is PlanetModel)
                process(dispatcherShip, collisionEvent.getOtherModel() as PlanetModel);
        }

        void process(ShipModel dispatcherShip, ShipModel collisionShip)
        {
            // if both ships are for different flocks ignore collision
            if(!dispatcherShip.getFlock().has(collisionShip))
            {
                // get dispatcher ship collider 
                Collider2D dispatcherShipCollider = dispatcherShip.gameObject.GetComponent<Collider2D>();

                // get second ship collider 
                Collider2D collisionShipCollider = collisionShip.gameObject.GetComponent<Collider2D>();

                // inform physics engine to ignore collision between colliders
                Physics2D.IgnoreCollision(dispatcherShipCollider, collisionShipCollider);
            }
        }

        void process(ShipModel dispatcherShip, PlanetModel collisionPlanet)
        {
            // if the ship collided or reached the target
            if(dispatcherShip.getTarget() == collisionPlanet)
            {
                // if the target is ally
                if(dispatcherShip.getFlock().getOwner() == collisionPlanet.getOwner())
                    manager.dispatch(new BoostEvent(dispatcherShip, collisionPlanet));
                // if the target is enemy
                else
                    manager.dispatch(new DamageEvent(dispatcherShip, collisionPlanet));
            }
        }

    }
}
