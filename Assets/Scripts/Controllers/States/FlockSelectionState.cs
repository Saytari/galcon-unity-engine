using System.Collections;
using System.Collections.Generic;

using Controllers.Enumerations;
using Models;

namespace Controllers.States
{
    public class FlockSelectionState : IState
    {
        // player who has this state
        HumanPlayerModel humanPlayerModel;
        
        // all selected planets
        List<FlockModel> selectedFlocks;
        
        // controller to send orders
        StateController controller;

        public FlockSelectionState (StateController controller, HumanPlayerModel humanPlayerModel)
        {
            // set player controller
            this.controller = controller;

            // set player instance
            this.humanPlayerModel = humanPlayerModel;

            selectedFlocks = humanPlayerModel.getSelectedFlocks();
        }

        public FlockSelectionState(StateController controller, HumanPlayerModel humanPlayerModel, FlockModel clickedFlock) : this(controller, humanPlayerModel)
        {
            selectedFlocks.Add(clickedFlock);
            humanPlayerModel.focus(clickedFlock);
        }

         // interface implementation
        public IState select(PlanetModel clickedPlanet, SelectMode mode)
        {
            controller.redirectTo(selectedFlocks, clickedPlanet);
            return retreat();
        }

        // interface implementation
        public IState select(FlockModel clickedFlock, SelectMode mode)
        {
            if(humanPlayerModel.hasFlock(clickedFlock))
            {
                if(mode == SelectMode.Action)
                {
                    resetList();
                    return new FlockSelectionState(controller, humanPlayerModel, clickedFlock);
                }
                else
                {
                    if(!selectedFlocks.Contains(clickedFlock))
                    {
                        selectedFlocks.Add(clickedFlock);
                        humanPlayerModel.focus(clickedFlock);
                    }
                    else
                    {
                        selectedFlocks.Remove(clickedFlock);
                        humanPlayerModel.unFocus(clickedFlock);
                        if(selectedFlocks.Count == 0)
                            return new PrimaryState(controller, humanPlayerModel);
                    }
                }
            }

            return this;
        }

        // interface implementation
        public IState selectAll()
        {
            // select all conquerd planets
            foreach(FlockModel activeFlock in humanPlayerModel.getActiveFlocks())
                if(!selectedFlocks.Contains(activeFlock))
                {
                    selectedFlocks.Add(activeFlock);
                    humanPlayerModel.focus(activeFlock);
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
            foreach(FlockModel flockAreSelected in selectedFlocks)
                humanPlayerModel.unFocus(flockAreSelected);
            selectedFlocks.Clear();
        }
    }
}