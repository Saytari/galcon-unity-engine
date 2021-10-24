using System.Collections;
using System.Collections.Generic;

using Controllers.Enumerations;
using Models;

namespace Controllers.States
{
    public class PlanetSelectionState : IState
    {
        // player who has this state
        HumanPlayerModel humanPlayerModel;
        
        // all selected planets
        List<PlanetModel> selectedPlanets;

        // controller to send orders
        StateController controller;
          
        public PlanetSelectionState(StateController controller, HumanPlayerModel humanPlayerModel)
        {
            // set player controller
            this.controller = controller;

            // set player instance
            this.humanPlayerModel = humanPlayerModel;

            selectedPlanets = humanPlayerModel.getSelectedPlanets();
        }

        public PlanetSelectionState(StateController controller, HumanPlayerModel humanPlayerModel, PlanetModel clickedPlanet) : this(controller, humanPlayerModel)
        {
            selectedPlanets.Add(clickedPlanet);
            humanPlayerModel.focus(clickedPlanet);
        }

        // interface implementation
        public IState select(PlanetModel clickedPlanet, SelectMode mode)
        {
            // if the planet is foreign 
            if(!humanPlayerModel.hasPlanet(clickedPlanet) || (!selectedPlanets.Contains(clickedPlanet) && mode == SelectMode.Action))
            {
                controller.attackOnPlanet(selectedPlanets, clickedPlanet);
                return retreat();
            }
            // if planet are selected and ctrl is pressed
            else if(selectedPlanets.Contains(clickedPlanet) && mode == SelectMode.Include)
            {
                selectedPlanets.Remove(clickedPlanet);
                    humanPlayerModel.unFocus(clickedPlanet);
                    // if the selection list is empty return to primary state
                    if(selectedPlanets.Count == 0)
                        return new PrimaryState(controller, humanPlayerModel);
            }
            // if planet are selected but ctrl is not pressed
            else if (selectedPlanets.Contains(clickedPlanet) && mode == SelectMode.Action)
            {
                // reset selected planets to unselected
                resetList();
                // convert to absolute new planet selection state
                return new PlanetSelectionState(controller, humanPlayerModel, clickedPlanet);
            }
            // if planet are not selected and ctrl is pressed
            else if(!selectedPlanets.Contains(clickedPlanet) && mode == SelectMode.Include)
            {
                selectedPlanets.Add(clickedPlanet);
                humanPlayerModel.focus(clickedPlanet);
            }
            
            return this;
        }

        // interface implementation
        public IState select(FlockModel clickedFlock, SelectMode mode)
        {
            if(humanPlayerModel.hasFlock(clickedFlock))
                return new FlockSelectionState(controller, humanPlayerModel, clickedFlock);
            else
                return this;
        }

        // interface implementation
        public IState selectAll()
        {
            // select all conquerd planets
            foreach(PlanetModel conquerdPlanet in humanPlayerModel.getConqueredPlanets())
                if(!selectedPlanets.Contains(conquerdPlanet))
                {
                    selectedPlanets.Add(conquerdPlanet);
                    humanPlayerModel.focus(conquerdPlanet);
                }
            return this; 
        }

        // interface implementation
        public IState retreat()
        {
            // unselect all selected planets
            resetList();
            // return primary state
            return new PrimaryState(controller, humanPlayerModel);
        }

        void resetList()
        {
            foreach(PlanetModel planetAreSelected in selectedPlanets)
                humanPlayerModel.unFocus(planetAreSelected);

            selectedPlanets.Clear();
        }
    }
}