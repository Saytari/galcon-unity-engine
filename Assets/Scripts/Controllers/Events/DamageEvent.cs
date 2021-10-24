using Models;
using Models.Contracts;

namespace Controllers.Events
{
    public class DamageEvent : IEvent
    {
        // ship that caused the event
        protected ShipModel shipToDamageBy;

        // planet to boost
        protected PlanetModel planetToDamage;

        public DamageEvent(ShipModel shipToDamageBy, PlanetModel planetToDamage)
        {
            // initialize ship model
            this.shipToDamageBy = shipToDamageBy;
            // initialize planet model
            this.planetToDamage = planetToDamage;
        }

        public ShipModel getShip()
        {
            return shipToDamageBy;
        }

        public PlanetModel getPlanet()
        {
            return planetToDamage;
        }
    }

}