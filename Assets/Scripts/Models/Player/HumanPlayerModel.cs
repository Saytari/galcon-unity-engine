using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers;

namespace Models
{
    public class HumanPlayerModel : PlayerModel
    {
        // selected conquredPlanets
        protected List<PlanetModel> selectedPlanets;

        // selected conquredPlanets
        protected List<FlockModel> selectedFlocks;

        void Awake()
        {
            // get custom planet type
            planetType = Settings.customPlanetType;

            // get custom ship type
            shipType = Settings.customShipType;
        }

        void Start()
        {
            // call parent start method
            base.Start();

            // allocate empty list
            selectedPlanets = new List<PlanetModel>();

            // allocate empty list
            selectedFlocks = new List<FlockModel>();
        }
        
        public override void removePlanet(PlanetModel planetToRemove)
        {
            if(selectedPlanets.Contains(planetToRemove))
            {
                selectedPlanets.Remove(planetToRemove);
                unFocus(planetToRemove);
            }

            base.removePlanet(planetToRemove);
        }

        public List<PlanetModel> getSelectedPlanets()
        {
            return selectedPlanets;
        }

        public List<FlockModel> getSelectedFlocks()
        {
            return selectedFlocks;
        }

        public void focus(PlanetModel planetToFocus)
        {
            planetToFocus.setSelection(true);
        }

        public void unFocus(PlanetModel planetToUnfocus)
        {
            planetToUnfocus.setSelection(false);
        }

        public void focus(FlockModel flockToFocus)
        {
            flockToFocus.setSelection(true);
        }

        public void unFocus(FlockModel flockToUnfocus)
        {
            flockToUnfocus.setSelection(false);
        }
        
    }
}