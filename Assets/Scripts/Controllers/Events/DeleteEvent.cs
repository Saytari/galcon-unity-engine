using Controllers.Enumerations;
using Models;

namespace Controllers.Events
{
    public class DeleteEvent : IEvent
    {
        // model that caused the event
        protected ShipModel shipToDelete;

        // delete mode
        protected DeleteMode mode;

        public DeleteEvent(ShipModel shipToDelete, DeleteMode mode)
        {
            // initialize model
            this.shipToDelete = shipToDelete;
            // initialize mode 
            this.mode = mode;
        }

        public ShipModel getShip()
        {
            return shipToDelete;
        }

        public DeleteMode getMode()
        {
            return mode;
        }
    }

}