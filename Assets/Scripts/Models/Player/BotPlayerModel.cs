using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models.Enumerations;

namespace Models
{
    public class BotPlayerModel : PlayerModel
    {
        // bot brain type
        public BrainType brainType;

        // enemies planets
        protected List<PlanetModel> enemiesPlanets;

        // enemies flocks
        protected List<FlockModel> enemiesFlocks;

        void Awake()
        {
            // set random planet type
            planetType = getRandomPlanetType();

            // set ship type
            shipType = getShipTypeFor(planetType);

            // create list for enemies planets
            enemiesPlanets = new List<PlanetModel>();

            // create list for enemies flocks
            enemiesFlocks = new List<FlockModel>();
        }

        void Start()
        {
            // call parent start method
            base.Start();

            // fill enemies planets list
            fillList();
        }

        PlanetType getRandomPlanetType()
        {
            // list to store all game planets types
            List<PlanetType> allTypes = new List<PlanetType>(Settings.planetsTypes);

            // remove human player planet type
            allTypes.Remove(Settings.customPlanetType);

            // remove raw planet type
            allTypes.Remove(PlanetType.GrayWhale);

            // set defaults available types 
            List<PlanetType> availablesType = new List<PlanetType>(allTypes);

            // list for all bots players
            BotPlayerModel[] players = FindObjectsOfType<BotPlayerModel>();

            // sort type availabilities
            foreach(PlanetType planetType in allTypes)
                foreach(BotPlayerModel player in players)
                    if(player.getPlanetType() == planetType)
                        availablesType.Remove(planetType);
            
            int planetTypeIndex = Random.Range(0, availablesType.Count - 1);

            return availablesType[planetTypeIndex];
        }

        ShipType getShipTypeFor(PlanetType planetType)
        {
            switch(planetType)
            {
                case PlanetType.RedOctopus:
                    return ShipType.Venus;
                case PlanetType.GreenCrocodile:
                    return ShipType.Lutos;
                case PlanetType.YellowFox:
                    return ShipType.Renown;
                default:
                    return ShipType.Pluto;
            }
        }

        void fillList()
        {
            // seek game planets
            PlanetModel[] gamePlanets = FindObjectsOfType<PlanetModel>();

            foreach(PlanetModel planetModel in gamePlanets)
                if(planetModel.getOwner() != this)
                    enemiesPlanets.Add(planetModel);
        }

        public override void addPlanet(PlanetModel planetToRemove)
        {
            base.addPlanet(planetToRemove);
            if(enemiesPlanets.Contains(planetToRemove))
                enemiesPlanets.Remove(planetToRemove);
        }

        public override void removePlanet(PlanetModel planetToRemove)
        {
            base.removePlanet(planetToRemove);
            if(!enemiesPlanets.Contains(planetToRemove))
                enemiesPlanets.Add(planetToRemove);
        }

        public List<PlanetModel> getEnemiesPlanets()
        {
            return enemiesPlanets;
        }

        public List<FlockModel> getEnemiesFlocks()
        {
            return enemiesFlocks;
        }

        public BrainType getBrainType()
        {
            return brainType;
        }

    }
}