using UnityEngine;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="BaseDialogueStateSO")]
    public class BaseDialogueStateSO : DialogueStateSO
    {
        private const Events.Event.EventTypes ContinueEvent = Events.Event.EventTypes.Continue;
        private const Events.Event.EventTypes ActivatedEvent = Events.Event.EventTypes.Activated;
        private const Events.Event.EventTypes UnactivatedEvent = Events.Event.EventTypes.Unactivated;
        private const Events.Event.EventTypes ChoiceEvent = Events.Event.EventTypes.BeginChoice;

        private int _activeElements = 0;


        public override void Reset()
        {
            _activeElements = 0;
        }

        public override void HandleEvent(Events.Event newEvent, Events.EventHandlersManager eventsManager)
        {
            switch (newEvent.EventType)
            {
                case ActivatedEvent:
                    _activeElements++;
                    break;
                case UnactivatedEvent:
                    _activeElements = (_activeElements > 0) ? _activeElements - 1 : 0;
                    break;
                case ContinueEvent:
                    eventsManager.AddEvent(ContinueEvent, newEvent);
                    break;
                case ChoiceEvent:
                    _activeElements++;
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
            else if (eventsManager.CanHandleEvent(ContinueEvent))
            {
                if (_activeElements > 0)
                {
                    dialogueRoot.InterruptPresentation();
                }
                else
                {
                    eventsManager.HandleEvent(ContinueEvent);
                }
                eventsManager.RemoveEvent(ContinueEvent);
            }
        }

        public override void StateUpdate(Events.EventHandlersManager eventsManager)
        {
            if (Input.anyKeyDown && !eventsManager.RegisteredEvent(ContinueEvent))
            {
                HandleEvent(new Events.Event(ContinueEvent), eventsManager);
            }
        }
    }
}