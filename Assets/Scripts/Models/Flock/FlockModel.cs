using System.Collections;
using System.Collections.Generic;

namespace Models
{
    public class FlockModel
    {
        // owner
        PlayerModel owner;

        // containing ships
        List<ShipModel> containedShips;

        public FlockModel(List<ShipModel> shipsToContains)
        {
            // fill flock with ships
            containedShips = shipsToContains;

            foreach(ShipModel shipModel in containedShips)
                shipModel.setFlock(this);
        }

        public void setOwner(PlayerModel owner)
        {
            this.owner = owner;
        }

        public PlayerModel getOwner()
        {
            return owner;
        }

        public void setSelection(bool focus)
        {
            foreach(ShipModel shipModel in containedShips)
                shipModel.setSelection(focus);
        }

        public List<ShipModel> getShips()
        {
            return containedShips;
        }

        public bool has(ShipModel shipToCheck)
        {
            return containedShips.Contains(shipToCheck);
        }

        public void removeShip(ShipModel shipToRemove)
        {
            containedShips.Remove(shipToRemove);
            if (containedShips.Count == 0)
                owner.removeFlock(this);
        }
    }
}