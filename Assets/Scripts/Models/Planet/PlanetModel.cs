using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models.Contracts;
using Models.Enumerations;
using Views.Contracts;

namespace Models
{
    public class PlanetModel : Model, IPlanetModel
    {
        // type
        public PlanetType type;

        // size
        public PlanetSize size;

        // max ships count
        public int capacity = 100;

        // max ships count to release per attack
        public int maxRelease;

        // ready ships count
        protected int shipsCount;

        // planet position
        protected Vector2 position;
        
         // select state
        protected bool isSelected = false;

        // planet view contract
        protected IPlanetView planetView;

        // planet owner
        protected PlayerModel owner;

        // get components attached to this object
        void Awake()
        {
            // get the planet view component attached to this planet instance
            planetView = GetComponent<IPlanetView>();
            
            // set current position
            position = new Vector2(transform.position.x, transform.position.y);
        }

        public PlanetType getType()
        {
            return type;
        }
        
        public int getCapacity()
        {
            return capacity;
        }

        public int getShipsCount()
        {
            return shipsCount;
        }
        
        public void setShipsCount(int count)
        {
            if(count < 0)
                shipsCount = 0;
            else if(count > capacity)
                shipsCount = capacity;
            else
                shipsCount = count;

            // inform view about ships count
            planetView.refreshCount(shipsCount);
        }

        public int getReadyShipsCount()
        {
            return shipsCount < maxRelease ? shipsCount : maxRelease;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setSelection(bool selectState)
        {
            this.isSelected = selectState;

            // inform view about the new selection state
            planetView.refreshSelection(selectState);
        }

        public void setOwner(PlayerModel newOwner)
        {
            owner = newOwner;

            // modify current planet type
            type = owner.getPlanetType();

            // inform view about the new type
            planetView.refreshType(type);
        }

        public PlayerModel getOwner()
        {
            return owner;
        }


    }
}
