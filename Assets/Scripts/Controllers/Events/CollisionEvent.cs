using Models.Contracts;

namespace Controllers.Events
{
    public class CollisionEvent : IEvent
    {
        protected IModel firstObject;

        protected IModel secondObject;

        public CollisionEvent(IModel firstObject, IModel secondObject)
        {
            this.firstObject = firstObject;
            this.secondObject = secondObject;
        }

        public IModel getModel()
        {
            return firstObject;
        }

        public IModel getOtherModel()
        {
            return secondObject;
        }
    }

}