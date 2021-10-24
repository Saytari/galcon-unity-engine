using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models.Enumerations;

namespace Models
{
    public class PlayerModel : Model
    {
        // player conquered planets
        protected List<PlanetModel> conqueredPlanets;

        // player active flocks
        protected List<FlockModel> activeFlocks;

        // planet type
        protected PlanetType planetType = PlanetType.GrayWhale;

        // ship type
        protected ShipType shipType;

        // the number of planets that has been conquerd during the game by this player
        protected int totalConquerdPlanets = 0;

        // the number of flocks that has been released during the game by this player
        protected int totalReleasedFlocks = 0;

        // the number of planets that has been released during the game by this player
        protected int totalReleasedShips = 0;

        protected void Start()
        {
            // pick up random planet
            PlanetModel randomPlanet = getRandomPlanet();

            // add this random planet to the conquered list
            conqueredPlanets.Add(randomPlanet);

            // set ower of this planet
            randomPlanet.setOwner(this);
        }



        public int getPlanetsCount()
        {
            return conqueredPlanets.Count;
        }

        public int getFlocksCount()
        {
            return activeFlocks.Count;
        }

        public int getShipsCount()
        {
            int total = 0;

            foreach(FlockModel flock in activeFlocks)
                total += flock.getShips().Count;

            return total;
        }
        
        public PlayerModel()
        {
            // create empty conquered list
            conqueredPlanets = new List<PlanetModel>();

            // create empty active flocks list
            activeFlocks = new List<FlockModel>();
        }

        public List<PlanetModel> getConqueredPlanets()
        {
            return conqueredPlanets;
        }

        public List<FlockModel> getActiveFlocks()
        {
            return activeFlocks;
        }

        public virtual void addPlanet(PlanetModel planetToAdd)
        {
            conqueredPlanets.Add(planetToAdd);
            increaseTotalPlanets(1);
        }

        public bool hasPlanet(PlanetModel planetToCheck)
        {
            return conqueredPlanets.Contains(planetToCheck);
        }

        public bool hasPlanets()
        {
            return conqueredPlanets.Count == 0 ? false : true;
        }

        public void addFlock(FlockModel flockToAdd)
        {
            activeFlocks.Add(flockToAdd);
            flockToAdd.setOwner(this);
            increaseTotalFlocks(1);
            increaseTotalShips(flockToAdd.getShips().Count);
        }

        public bool hasFlock(FlockModel flockToCheck)
        {
            return activeFlocks.Contains(flockToCheck);
        }

        public bool hasFlocks()
        {
            return activeFlocks.Count == 0 ? false : true;
        }

        public void setPlanetType(PlanetType planetType)
        {
            this.planetType = planetType;
        }

        public void setShipType(ShipType shipType)
        {
            this.shipType = shipType;
        }

        public PlanetType getPlanetType()
        {
            return planetType;
        }

        public ShipType getShipType()
        {
            return shipType;
        }

        public virtual void removePlanet(PlanetModel planetToRemove)
        {
            if(hasPlanet(planetToRemove))
                conqueredPlanets.Remove(planetToRemove);
        }

        public void removeFlock(FlockModel flockToRemove)
        {
            activeFlocks.Remove(flockToRemove);
        }

        protected PlanetModel getRandomPlanet()
        {
            // game planets
            PlanetModel[] planetModels = FindObjectsOfType<PlanetModel>();

            // list to store availables planets
            List<PlanetModel> availablePlanets = new List<PlanetModel>();

            // sort planets according to the owner
            foreach(PlanetModel planetModel in planetModels)
                // if the planet has no owner
                if(planetModel.getType() == PlanetType.GrayWhale)
                    // add this planet to availables planets
                    availablePlanets.Add(planetModel);

            // choose random planet index
            int randomPlanetIndex = Random.Range(0, availablePlanets.Count - 1);

            // pick up random planet
            return availablePlanets[randomPlanetIndex];
        }


        protected void increaseTotalPlanets(int amount)
        {
            totalConquerdPlanets += amount;
        }

        protected void increaseTotalFlocks(int amount)
        {
            totalReleasedFlocks += amount;
        }

        protected void increaseTotalShips(int amount)
        {
            totalReleasedShips += amount;
        }

        public int getTotalPlanets()
        {
            return totalConquerdPlanets;
        }

        public int getTotalFlocks()
        {
            return totalReleasedFlocks;
        }

        public int getTotalShips()
        {
            return totalReleasedShips;
        }

        public void flushFlocks()
        {
            activeFlocks.Clear();
        }
    }
}
