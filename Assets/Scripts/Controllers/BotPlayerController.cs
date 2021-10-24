using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Brains;
using Controllers.Events;
using Models;
using Models.Enumerations;

namespace Controllers
{
    public class BotPlayerController : MonoBehaviour
    {
        // avarege decision time
        public float avaregeDecisionTime = 5f;

        // bot next decision time
        float nextDecisionTime;

        // bot player model
        BotPlayerModel botModel;

        // bot brain 
        Brain brain;
        
        // player controller
        Manager manager;


        // Start is called before the first frame update
        void Awake()
        {
            // get human model component attached to this instance
            botModel = GetComponent<BotPlayerModel>();

            // get player controller component
            manager = FindObjectOfType<Manager>();

            // setup next decision time
            nextDecisionTime = generateNextDecisionTime();

            // initialize brain
            brain = detectBrain(botModel.getBrainType());
        }

        void Update()
        {
            if(Time.time > nextDecisionTime)
            {
                IPlayerEvent[] playerEvents = brain.resolveEvents();
                if(playerEvents != null)
                    dispatchToManager(playerEvents);
                nextDecisionTime = generateNextDecisionTime();
            }

            if(botModel.getShipsCount() == 0)
                botModel.flushFlocks();
        }

    
        float generateNextDecisionTime()
        {
            // return random decision time
            return Time.time + Random.Range(0, avaregeDecisionTime * 2);
        }

        void dispatchToManager(IPlayerEvent[] playerEvents)
        {
            // cast and sort events
            foreach(IPlayerEvent playerEvent in playerEvents)
            {
                if(playerEvent is EmersionEvent)
                    manager.dispatch(playerEvent as EmersionEvent);
                else if (playerEvent is RedirectEvent)
                    manager.dispatch(playerEvent as RedirectEvent);

            }
        }

        protected Brain detectBrain(BrainType brainType)
        {
            switch(brainType)
            {
                case BrainType.Advanced:
                    return new AdvancedBrain(botModel);
                case BrainType.Expert:
                    return new ExpertBrain(botModel);
                default:
                    return new SimpleBrain(botModel);
            }
        }
    }
}
