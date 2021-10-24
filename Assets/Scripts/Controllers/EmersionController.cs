using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;
using Models.Enumerations;

namespace Controllers
{
    public class EmersionController : MonoBehaviour
    {
        // ships prefabs path
        protected string prefabPath = "Prefabs/Ships/";

        // handle incoming event
        public void handle(EmersionEvent emersionEvent)
        {
            // get dispatching player who publish this event
            PlayerModel dispatcher = emersionEvent.getDispatcher();

            // get planet to pop up ships from
            PlanetModel sourcePlanet = emersionEvent.getSource();

            // get planet to pop up ships for
            PlanetModel targetPlanet = emersionEvent.getTarget();

            // ship type
            ShipType shipType = dispatcher.getShipType();

            // creating empty list to store created ships
            List<ShipModel> createdShips = generateFlock(shipType, sourcePlanet, targetPlanet);

            // create new flock for the dispatcher
            dispatcher.addFlock(new FlockModel(createdShips));
        }

        List<ShipModel> generateFlock(ShipType shipType, PlanetModel sourcePlanet, PlanetModel targetPlanet)
        {
            // source planet ready ships count
            // note: ready ships is not the same as current ships 
            int readyShipsCount = sourcePlanet.getReadyShipsCount();

            // source planet position
            Vector2 sourcePosition = sourcePlanet.getPosition();

            // create empty list to return
            List<ShipModel> createdShips = new List<ShipModel>();

            for(int shipIndex = 0; shipIndex < readyShipsCount; shipIndex++)
            {   
                // ship offset so it does not overlap with another ship
                float offset = 1f + shipIndex / 1000f;

                // calculate ship position according to the source position
                Vector3 shipPosition = new Vector3(sourcePosition.x + offset, sourcePosition.y + offset, 0);
                
                // generate ship with desired type & position
                ShipModel createdShipModel = generateShip(shipType, shipPosition);

                // set destination point
                createdShipModel.setDestination(targetPlanet.getPosition());

                // set target object
                createdShipModel.setTarget(targetPlanet);

                // add to list
                createdShips.Add(createdShipModel);
            }

            // calculate remains ships after emersions
            int remainsShips = sourcePlanet.getShipsCount() - readyShipsCount;

            // decrease source planet ships count
            sourcePlanet.setShipsCount(remainsShips);
            
            return createdShips;
        }

        ShipModel generateShip(ShipType shipType, Vector2 shipPosition)
        {
            UnityEngine.Object prefab = Resources.Load(prefabPath + shipType);

            GameObject createdShipObject = Instantiate(prefab, shipPosition, Quaternion.identity) as GameObject;

            return createdShipObject.GetComponent<ShipModel>();
        }
    }
}