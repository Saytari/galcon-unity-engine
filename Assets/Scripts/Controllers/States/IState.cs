using Controllers.Enumerations;
using Models;

namespace Controllers.States
{
    public interface IState
    {
        IState select(PlanetModel selectedPlanet, SelectMode mode);
        IState select(FlockModel selectedFlock, SelectMode mode);
        IState selectAll();
        IState retreat();
    }
}