using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Enumerations;

namespace Views.Contracts
{
    public interface IShipView
    {

        void refreshSelection(bool focus);

        void fadeOut(DeleteMode mode);
    }
}
