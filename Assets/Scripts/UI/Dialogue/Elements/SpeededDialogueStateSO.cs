using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UI.Dialogue.Events;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="SpeededDialogueStateSO")]
    public class SpeededDialogueStateSO : DialogueStateSO
    {
        [SerializeField] private float _intervalTimeSeconds = 500f;

        private const Events.Event.EventTypes ContinueEvent = Events.Event.EventTypes.Continue;
        private const Events.Event.EventTypes ChoiceEvent = Events.Event.EventTypes.BeginChoice;
        private const Events.Event.EventTypes SpeedDownEvent = Events.Event.EventTypes.SpeedDown;

        private float _currentTimeSeconds = -1;


        public override void HandleEvent(Events.Event newEvent, EventHandlersManager eventsManager)
        {
            switch (newEvent.EventType)
            {
                case ChoiceEvent:
                case SpeedDownEvent:
                case ContinueEvent:
                    eventsManager.AddEvent(newEvent.EventType, newEvent);
                    break;
            }
        }

        public override void StateLateUpdate(DialogueUIRoot dialogueRoot, EventHandlersManager eventsManager)
        {
            if (eventsManager.CanHandleEvent(ChoiceEvent))
            {
                eventsManager.HandleEvent(ChoiceEvent);
                eventsManager.ClearEvents();
            }
            else if (eventsManager.CanHandleEvent(SpeedDownEvent))
            {
                eventsManager.HandleEvent(SpeedDownEvent);
                eventsManager.ClearEvents();
            }
            else if (eventsManager.CanHandleEvent(ContinueEvent))
            {
                eventsManager.HandleEvent(ContinueEvent);
                eventsManager.RemoveEvent(ContinueEvent);
            }
        }

        public override void StateUpdate(EventHandlersManager eventsManager)
        {
            _currentTimeSeconds = (_currentTimeSeconds < 0) ? Time.realtimeSinceStartup : _currentTimeSeconds;
            if (!eventsManager.RegisteredEvent(ContinueEvent) &&
            Time.realtimeSinceStartup - _currentTimeSeconds >= _intervalTimeSeconds)
            {
                _currentTimeSeconds = Time.realtimeSinceStartup;
                HandleEvent(new Events.Event(ContinueEvent), eventsManager);
            }
        }

        public override void Reset()
        {
            _currentTimeSeconds = -1;
        }
    }
}
