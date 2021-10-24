using UnityEngine;

using Controllers.Events;
using Controllers.Enumerations;
using Models;

namespace Controllers
{
    public class DeleteController : MonoBehaviour
    {

        public void handle(DeleteEvent deleteEvent)
        {
            // ship to delete
            ShipModel shipToDelete = deleteEvent.getShip();

            // delete mode
            DeleteMode mode = deleteEvent.getMode();
            
            // delete model
            shipToDelete.delete(mode);
        }
    }
}
