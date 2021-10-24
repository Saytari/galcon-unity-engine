using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;
using Models.Enumerations;

namespace Controllers.Brains
{
    public abstract class Brain
    {
        // player model
        protected BotPlayerModel botPlayer;

        // player planets
        protected List<PlanetModel> conqueredPlanets;

        // player flocks
        protected List<FlockModel> activeFlocks;

        // enemies planets
        protected List<PlanetModel> enemeisPlanets;

        // enemies flocks
        protected List<FlockModel> enemiesFlock;

        public Brain(BotPlayerModel botPlayer)
        {
            // set the bot player
            this.botPlayer = botPlayer;
            // get conquered planets list
            conqueredPlanets = botPlayer.getConqueredPlanets();
            // get active flocks list
            activeFlocks = botPlayer.getActiveFlocks();
            // get enemies planets list
            enemeisPlanets = botPlayer.getEnemiesPlanets();
            // get enemies flocksP list
            enemiesFlock = botPlayer.getEnemiesFlocks();
        }

        

        public abstract IPlayerEvent[] resolveEvents();

    }
}