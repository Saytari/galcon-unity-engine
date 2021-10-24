using Models;
using Models.Contracts;

namespace Controllers.Events
{
    public class BoostEvent : IEvent
    {
        // ship that caused the event
        protected ShipModel shipToBoostBy;

        // planet to boost
        protected PlanetModel planetToBoost;

        public BoostEvent(ShipModel shipToBoostBy, PlanetModel planetToBoost)
        {
            // initialize ship model
            this.shipToBoostBy = shipToBoostBy;
            // initialize planet model
            this.planetToBoost = planetToBoost;
        }

        public ShipModel getShip()
        {
            return shipToBoostBy;
        }

        public PlanetModel getPlanet()
        {
            return planetToBoost;
        }
    }

}