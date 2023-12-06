using UnityEngine;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="ChoiceDialogueStateSO")]
    public class ChoiceDialogueStateSO : DialogueStateSO
    {
        private const Events.Event.EventTypes ChoiceEvent = Events.Event.EventTypes.EndChoice;


        public override void HandleEvent(Events.Event newEvent, Events.EventHandlersManager eventsManager)
        {
            switch (newEvent.EventType)
            {
                case ChoiceEvent:
                    eventsManager.AddEvent(ChoiceEvent, newEvent);
                    break;
            }
        }

        public override void StateLateUpdate(DialogueUIRoot dialogueRoot, Events.EventHandlersManager eventsManager)
        {
            if (eventsManager.CanHandleEvent(ChoiceEvent))
            {
                eventsManager.HandleEvent(ChoiceEvent);
                eventsManager.ClearEvents();
            }
        }

        public override void StateUpdate(Events.EventHandlersManager eventsManager) { }
    }
}
