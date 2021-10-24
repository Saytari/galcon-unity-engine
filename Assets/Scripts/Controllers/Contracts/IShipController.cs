using System.Collections;
using System.Collections.Generic;

using Controllers.Events;

namespace Controllers.Contracts
{
    public interface IShipController
    {
        void dispatch(InputEvent inputEvent);

        void dispatch(CollisionEvent collisionEvent);
    }
}
