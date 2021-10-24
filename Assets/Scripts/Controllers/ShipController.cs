using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Contracts;
using Controllers.Events;
using Controllers.Enumerations;
using Models;
using Models.Contracts;
using Views;

namespace Controllers
{
    public class ShipController : MonoBehaviour, IShipController
    {
    
        // moving speed
        public float moveSpeed = 5;

        // rotate speed
        public float rotateSpeed = 1f;

        // fade out ship
        protected bool moveShip = true;

        // shipModel is ready
        protected bool modelIsReady = false;

        // define rotate desicion
        protected enum RotateDesicion
        {
            NoRotate,
            RotateLeft,
            RotateRight
        }

        // rigidbody component
        protected Rigidbody2D rigidBody;

        // ship model
        protected ShipModel shipModel;

        // game manager
        protected Manager manager;

        // start is called before the first frame update
        void Start()
        {
            // get the ship model component attached to this ship instance
            shipModel = GetComponent<ShipModel>();

            // get manager object
            manager = FindObjectOfType<Manager>();

            // get rigidbody componenet
            rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(!modelIsReady)
            {
                // store current position in shipModel
                shipModel.setPosition(rigidBody.position);

                // shipModel now is ready
                modelIsReady = true;
            }

            if(moveShip)
            {
                // change ship position
                move();

                // fix ship rotation
                fixOrientation();
            }
        }

        protected void move()
        {
            // calculate distance vector
            Vector2 distance = shipModel.getDistenation() - shipModel.getPosition();

            // calculate velocity vector
            Vector2 velocity = distance.normalized * moveSpeed * Time.deltaTime;

            // move rigid body according to the velocity
            rigidBody.MovePosition(shipModel.getPosition() + velocity);

            // change ship shipModel stored position
            shipModel.setPosition(rigidBody.position);
        }

        void fixOrientation()
        {
            // current ship orientation
            Vector2 shipOrientation = shipModel.getOrientation();

            // calculate distance vector
            Vector2 distance = shipModel.getDistenation() - shipModel.getPosition();

            // test rotate
            RotateDesicion desicion = testRotate(shipOrientation, distance);

            // if the test desicion is to rotate some where
            if(desicion != RotateDesicion.NoRotate)
            {
                if(desicion == RotateDesicion.RotateLeft)
                {
                    transform.Rotate(0f, 0f, rotateSpeed);
                    shipModel.setOrientation(Rotate(shipOrientation, rotateSpeed));
                }

                else
                {
                    transform.Rotate(0f, 0f, -rotateSpeed);
                    shipModel.setOrientation(Rotate(shipOrientation, -rotateSpeed));
                }
            }
        }

        RotateDesicion testRotate(Vector2 orientation, Vector2 distance)
        {
            // calculate angle between vectors
            float vectorsAngle = Vector2.Angle(orientation, distance);
            
            // if angle is zero return no rotate desicion
            if(vectorsAngle == 0f)
                return RotateDesicion.NoRotate;
            
            // rotate ship vector to determine the rotate desicion
            Vector2 testingOrientation = Rotate(orientation, 1);

            float newAngle = Vector2.Angle(testingOrientation, distance);

            if(newAngle < vectorsAngle)
                return RotateDesicion.RotateLeft;
            else
                return RotateDesicion.RotateRight;
        }


        Vector2 Rotate(Vector2 aPoint, float aDegree)
        {
            float rad = aDegree * Mathf.Deg2Rad;
            float s = Mathf.Sin(rad);
            float c = Mathf.Cos(rad);
            return new Vector2(
                aPoint.x * c - aPoint.y * s,
                aPoint.y * c + aPoint.x * s
            );
        }
        

        // interface implementation
        public void dispatch(InputEvent inputEvent)
        {
            manager.dispatch(inputEvent);
        }

        // interface implementation
        public void dispatch(CollisionEvent collisionEvent)
        {
            manager.dispatch(collisionEvent);
        }
    }
}
