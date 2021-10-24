using Models;
using Models.Contracts;

namespace Controllers.Events
{
    public class EmersionEvent : IPlayerEvent
    {
        protected PlayerModel dispatcher;

        protected PlanetModel sourcePlanet;

        protected PlanetModel targetPlanet;

        public EmersionEvent(PlayerModel dispatcher, PlanetModel sourcePlanet, PlanetModel targetPlanet)
        {
            this.dispatcher = dispatcher;
            this.sourcePlanet = sourcePlanet;
            this.targetPlanet = targetPlanet;
        }

        // interface implementation
        public PlayerModel getDispatcher()
        {
            return dispatcher;
        }

        public PlanetModel getSource()
        {
            return sourcePlanet;
        }

        public PlanetModel getTarget()
        {
            return targetPlanet;
        }
    }

}