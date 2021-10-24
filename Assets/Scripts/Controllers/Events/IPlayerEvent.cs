using Models;

namespace Controllers.Events
{
    public interface IPlayerEvent : IEvent
    {
        // player how dispatch this event
        PlayerModel getDispatcher();
    }
}