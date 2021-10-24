using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;

namespace Controllers.Brains
{
    public class SimpleBrain : Brain
    {
        public SimpleBrain(BotPlayerModel botModel) : base(botModel)
        {

        }
        
        // interface implementation
        public override IPlayerEvent[] resolveEvents()
        {
            if(conqueredPlanets.Count == 0)
                return null;
            // create new empty events list
            List<IPlayerEvent> eventsToReturn = new List<IPlayerEvent>();

            eventsToReturn.Add(randomEmersion());

            return eventsToReturn.ToArray();
        }

        protected EmersionEvent randomEmersion()
        {
            // merge lists to get all planet in one list
            List<PlanetModel> allPlanets = concatPlanetsList(conqueredPlanets, enemeisPlanets);

            // random indexes
            int sourcePlanetIndex = Random.Range(0, conqueredPlanets.Count );
            int destPlanetIndex = randomIndexExcept(0, allPlanets.Count , sourcePlanetIndex);


            // planet to start the attack
            PlanetModel sourcePlanet = conqueredPlanets[sourcePlanetIndex];

            // planet to attack on 
            PlanetModel destPlanet = allPlanets[destPlanetIndex];

            return new EmersionEvent(botPlayer, sourcePlanet, destPlanet);
        }

        protected int randomIndexExcept(int min, int max, int except)
        {
            int result = Random.Range(min, max-1);

            if (result >= except)
            result += 1;

            return result;
        }

        protected List<PlanetModel> concatPlanetsList(List<PlanetModel> firstList, List<PlanetModel> secondList)
        {
            List<PlanetModel> listForAllPlanets = new List<PlanetModel>();
            foreach(PlanetModel planetModel in firstList)
                listForAllPlanets.Add(planetModel);
            
            foreach(PlanetModel planetModel in secondList)
                listForAllPlanets.Add(planetModel);

            return listForAllPlanets;
        }
    }
}