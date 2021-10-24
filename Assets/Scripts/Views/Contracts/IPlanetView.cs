using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models.Enumerations;

namespace Views.Contracts
{
    public interface IPlanetView
    {
        // refresh counter
        void refreshCount(int count);

        // refresh focus state
        void refreshSelection(bool selectState);

        // refresh planet type
        void refreshType(PlanetType newTypes);
    }
}
