using Models;
using Models.Contracts;

namespace Controllers.Events
{
    public class RedirectEvent : IPlayerEvent
    {
        protected PlayerModel dispatcher;

        protected FlockModel flockToRedirect;

        protected PlanetModel planetToRedirectTo;

        public RedirectEvent(PlayerModel dispatcher, FlockModel flockToRedirect, PlanetModel planetToRedirectTo)
        {
            this.dispatcher = dispatcher;
            this.flockToRedirect = flockToRedirect;
            this.planetToRedirectTo = planetToRedirectTo;
        }

        // interface implementation
        public PlayerModel getDispatcher()
        {
            return dispatcher;
        }

        public FlockModel getFlock()
        {
            return flockToRedirect;
        }

        public PlanetModel getTarget()
        {
            return planetToRedirectTo;
        }
    }

}