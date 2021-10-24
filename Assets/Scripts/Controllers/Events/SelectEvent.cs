using Controllers.Enumerations;
using Models;
using Models.Contracts;

namespace Controllers.Events
{
    public class SelectEvent : InputEvent
    {
        // model whitch dispatch this event
        protected IModel modelToSelect;

        // select mode
        protected SelectMode mode;

        public SelectEvent(IModel modelToSelect, SelectMode mode)
        {
            this.modelToSelect = modelToSelect;

            this.mode = mode;
        }

        public IModel getModel()
        {
            return modelToSelect;
        }

        public SelectMode getMode()
        {
            return mode;
        }
    }

}