using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Controllers.States;
using Models;
using Models.Contracts;
namespace Controllers
{
    public class StateController : MonoBehaviour
    {
        // current selection state
        IState presentState;

        // human player model
        HumanPlayerModel model;

        // game manager
        Manager manager;


        // Start is called before the first frame update
        void Start()
        {
            // get human model component attached to this instance
            model = GetComponent<HumanPlayerModel>();

            // get playerModel controller component
            manager = FindObjectOfType<Manager>();

            // set default state to primary state
            presentState = new PrimaryState(this, model);

        }

        void Update()
        {
            if(Input.GetKey("space"))
                manager.dispatch(new SelectAllEvent());
            
            if(Input.GetKey("escape"))
                manager.dispatch(new RetreatEvent());

            if(model.getShipsCount() == 0)
                model.flushFlocks();
        }
        
        
        public void handle(InputEvent playerEvent)
        {
            if(playerEvent is SelectEvent)
            {
                SelectEvent selectEvent = playerEvent as SelectEvent;
                IModel selectedModel = selectEvent.getModel();
                if(selectedModel is PlanetModel)
                    presentState = presentState.select(selectEvent.getModel() as PlanetModel, selectEvent.getMode());
                else if(selectedModel is ShipModel)
                    presentState = presentState.select((selectedModel as ShipModel).getFlock(), selectEvent.getMode());
            }
            else if(playerEvent is SelectAllEvent)
                presentState = presentState.selectAll();
            else if(playerEvent is RetreatEvent)
                presentState = presentState.retreat();
        }


        public void attackOnPlanet(List<PlanetModel> planetsToAttackfrom, PlanetModel planetToAttackOn)
        {
            foreach(PlanetModel sourcePlanet in planetsToAttackfrom)
                manager.dispatch(new EmersionEvent(model, sourcePlanet, planetToAttackOn));
        }

        public void redirectTo(List<FlockModel> flocksToRedirect, PlanetModel planetToRedirectTo)
        {
            foreach(FlockModel flockToRedirect in flocksToRedirect)
                manager.dispatch(new RedirectEvent(model, flockToRedirect, planetToRedirectTo));
        }
        
    }
}
