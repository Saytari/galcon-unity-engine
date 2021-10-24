using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Enumerations;
using Models.Contracts;
using Models.Enumerations;
using Views.Contracts;

namespace Models
{
    public class ShipModel : Model, IShipModel
    {
        // for appearance
        public ShipType type;

        // body rotate
        protected float rotateAngle = 0f;
        
        // position
        protected Vector2 position;

        // destination
        protected Vector2 destination;

        // orientation
        protected Vector2 orientation;

        // target planet
        protected PlanetModel targetPlanet;

        //  focus state
        protected bool isSelected = false;

        // ship view contract
        protected IShipView shipView;
        
        // containing flock
        protected FlockModel flock;

        // get components  attached to this ship instance
        void Awake()
        {
            // get the ship view component
            shipView = GetComponent<IShipView>();
        }

        public ShipModel()
        {
            orientation = new Vector2(0, 1);
        }

        public void setOrientation(Vector2 orientation)
        {
            this.orientation = orientation;
        }

        public Vector2 getOrientation()
        {
            return orientation;
        }

        public void setFlock(FlockModel flock)
        {
            this.flock = flock;
        }

        public FlockModel getFlock()
        {
            return flock;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setDestination(Vector2 destination)
        {
            this.destination = destination;
        }

        public Vector2 getDistenation()
        {
            return destination;
        }

        public void setTarget(PlanetModel targetPlanet)
        {
            this.targetPlanet = targetPlanet;
        }

        public PlanetModel getTarget()
        {
            return targetPlanet;
        }

        public void setSelection(bool selectState)
        {
            this.isSelected = isSelected;
            shipView.refreshSelection(selectState);
        }
        
        public void delete(DeleteMode mode)
        {
            flock.removeShip(this);

            shipView.fadeOut(mode);
        }

    }   
}
