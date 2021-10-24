using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;

namespace Controllers.Brains
{
    public class AdvancedBrain : Brain
    {
        public AdvancedBrain(BotPlayerModel botModel) : base(botModel)
        {

        }

        // interface implementation
        public override IPlayerEvent[] resolveEvents()
        {
            if(conqueredPlanets.Count == 0 || enemeisPlanets.Count == 0)
                return null;
            // create new empty events list
            List<IPlayerEvent> eventsToReturn = new List<IPlayerEvent>();

            eventsToReturn.Add(randomAttackEmersion());

            return eventsToReturn.ToArray();
        }

        protected EmersionEvent randomAttackEmersion()
        {
            // random indexes
            int attackerIndex = Random.Range(0, conqueredPlanets.Count);
            int vectimIndex = Random.Range(0, enemeisPlanets.Count);


            // planet to start the attack
            PlanetModel planetToAttackFrom = conqueredPlanets[attackerIndex];

            // planet to attack on 
            PlanetModel planetToAttackOn = enemeisPlanets[vectimIndex];

            return new EmersionEvent(botPlayer, planetToAttackFrom, planetToAttackOn);
        }
    }
}