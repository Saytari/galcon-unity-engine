using System.Collections;
using System.Collections.Generic;

using Controllers.Enumerations;
using Models;

namespace Controllers.States
{
    public class PrimaryState : IState
    {
        // player who has this state
        HumanPlayerModel humanPlayerModel;
        
        // controller to send orders
        StateController controller;

        public PrimaryState(StateController controller, HumanPlayerModel humanPlayerModel)
        {
            // set player controller
            this.controller = controller;
            
            // set player instance
            this.humanPlayerModel = humanPlayerModel;
        }

        // interface implementation
        public IState select(PlanetModel clickedPlanet, SelectMode mode)
        {
            // if the player own this planet
            if(humanPlayerModel.hasPlanet(clickedPlanet))
                return new PlanetSelectionState(controller, humanPlayerModel, clickedPlanet);
            else
                // stay in the same state
                return this;
        }

        // interface implementation
        public IState select(FlockModel clickedFlock, SelectMode mode)
        {
            // if the player own this flock
            if(humanPlayerModel.hasFlock(clickedFlock))
                return new FlockSelectionState(controller, humanPlayerModel, clickedFlock);
            else
                // stay in the same state
                return this;
        }

        // interface implementation
        public IState selectAll()
        {
            // if player conquered planets is not zero select all of them
            if(humanPlayerModel.hasPlanets())
            {
                // create next state
                PlanetSelectionState nextState = new PlanetSelectionState(controller, humanPlayerModel);
                nextState.selectAll();
                return nextState;
            }
            // if player active flocks is not zero select all of them
            else if(humanPlayerModel.hasFlocks())
            {
                // create next state
                FlockSelectionState nextState = new FlockSelectionState(controller, humanPlayerModel);
                nextState.selectAll();
                return nextState;
            }
            // if has nothing stay in this state
            else
                return this;
        }

        // interface implementation
        public IState retreat()
        {
            return this;
        }
    }
}