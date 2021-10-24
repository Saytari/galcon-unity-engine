using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Events;
using Models;
using Models.Enumerations;

namespace Controllers
{
    public class Manager : MonoBehaviour
    {
        // emersions controller
        EmersionController emersionController;

        // redirects controller
        RedirectController redirectController;

        // collisions Controller
        CollisionController collisionController;

        // human player controller
        StateController stateController;

        // boost controller
        BoostController boostController;

        // damage controller
        DamageController damageController;
        
        // delete controller
        DeleteController deleteController;

        // end controller
        EndController endController;

        // all game players
        PlayerModel[] players;

        // get needed controllers & models
        void Awake()
        {
            // get redirect controller component
            emersionController = GetComponent<EmersionController>();

            // get redirect controller component
            redirectController = GetComponent<RedirectController>();

            // get collision controller component
            collisionController = GetComponent<CollisionController>();

            // get boost controller component
            boostController = GetComponent<BoostController>();

            // get damage controller component
            damageController = GetComponent<DamageController>();

            // get damage controller component
            deleteController = GetComponent<DeleteController>();

            // get human player state controller
            stateController = FindObjectOfType<StateController>();

            // get end controller
            endController = GetComponent<EndController>();
            
            // add bots players to the scene
            setupBots();

            // get all players
            players = FindObjectsOfType<PlayerModel>();
        }

        protected void setupBots()
        {
            // bot brain type
            BrainType botBrainType = Settings.brainType;

            // get bot prefab
            UnityEngine.Object botPrefab = Resources.Load("Prefabs/Players/" + botBrainType + "Bot");

            for(int botIndex = 0; botIndex < Settings.botsCount; botIndex++)
                Instantiate(botPrefab);
        }

        /***********************************************************
                           Events Routing Methods
        ************************************************************/
        
        public virtual void dispatch(EmersionEvent emersionEvent)
        {
            // route emersion event into its controller
            emersionController.handle(emersionEvent);
        }

        public virtual void dispatch(RedirectEvent redirectEvent)
        {
            // route redirect event into its controller
            redirectController.handle(redirectEvent);
        }

        public virtual void dispatch(CollisionEvent collisionEvent)
        {
            // route collision event into its controller
            collisionController.handle(collisionEvent);
        }
        
        public virtual void dispatch(InputEvent inputEvent)
        {
            // route input events into its controller
            stateController.handle(inputEvent);
        }

        public virtual void dispatch(BoostEvent boostEvent)
        {
            // route boost events into its controller
            boostController.handle(boostEvent);
        }

        public virtual void dispatch(DamageEvent damageEvent)
        {
            // route damage events into its controller
            damageController.handle(damageEvent);
        }

        public virtual void dispatch(DeleteEvent deleteEvent)
        {
            // route delete events into its controller
            deleteController.handle(deleteEvent);

            // scane players state
            endController.scane(players);
        }
    }
}