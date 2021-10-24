using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;
using Models.Enumerations;

namespace Controllers
{
    public class RedirectController : MonoBehaviour
    {
        // handle incoming event
        public void handle(RedirectEvent redirectEvent)
        {
            // flock that want to be redirect
            FlockModel flockToRedirect = redirectEvent.getFlock();

            // target planet
            PlanetModel targetPlanet = redirectEvent.getTarget();

            // point that flock must be redirect to
            Vector2 redirectPoint = targetPlanet.getPosition();
            
            foreach(ShipModel shipModel in flockToRedirect.getShips())
            {
                // set ship destination point
                shipModel.setDestination(redirectPoint);

                // set ship target planet
                shipModel.setTarget(targetPlanet);
            }  
        }
    }
}