using UnityEngine;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="ChoiceDialogueStateSO")]
    public class ChoiceDialogueStateSO : DialogueStateSO
    {
        private const Events.Event.EventTypes ChoiceEvent = Events.Event.EventTypes.EndChoice;
        private const Events.Event.EventTypes ExitEvent = Events.Event.EventTypes.Exit;


        public override void HandleEvent(Events.Event newEvent, Events.EventHandlersManager eventsManager)
        {
            switch (newEvent.EventType)
            {
                case ChoiceEvent:
                case ExitEvent:
                    eventsManager.AddEvent(newEvent.EventType, newEvent);
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
            else if (eventsManager.CanHandleEvent(ExitEvent))
            {
                eventsManager.HandleEvent(ExitEvent);
                eventsManager.ClearEvents();
            }
        }

        public override void StateUpdate(Events.EventHandlersManager eventsManager) { }
    }
}
