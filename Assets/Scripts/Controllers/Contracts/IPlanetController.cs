using System.Collections;
using System.Collections.Generic;

using Controllers.Events;

namespace Controllers.Contracts
{
    public interface IPlanetController
    {
        void dispatch(InputEvent inputEvent);
    }
}
