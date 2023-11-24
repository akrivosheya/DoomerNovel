using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events = UI.Dialogue.Events;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="BaseDialogueStateSO")]
    public class BaseDialogueStateSO : DialogueStateSO
    {
        private const Events.Event.EventTypes ContinueEvent = Events.Event.EventTypes.Continue;
        private const Events.Event.EventTypes ActivatedEvent = Events.Event.EventTypes.Activated;
        private const Events.Event.EventTypes UnactivatedEvent = Events.Event.EventTypes.Unctivated;
        private Dictionary<Events.Event.EventTypes, Events.Event> _currentEvents = new Dictionary<Events.Event.EventTypes, Events.Event>();

        private int _activeElements = 0;


        public override void HandleEvent(Events.Event newEvent)
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
                    _currentEvents.Add(ContinueEvent, newEvent);
                    break;
            }
        }

        public override void StateLateUpdate(Dictionary<Events.Event.EventTypes, Action<Events.Event>> handlers, DialogueUIElement dialogueRoot)
        {
            if (_currentEvents.ContainsKey(ContinueEvent) && handlers.ContainsKey(ContinueEvent))
            {
                if (_activeElements > 0)
                {
                    dialogueRoot.InterruptPresentation();
                }
                else
                {
                    handlers[ContinueEvent](_currentEvents[ContinueEvent]);
                }
            }

            _currentEvents.Clear();
        }

        public override void StateUpdate()
        {
            if (Input.anyKeyDown && !_currentEvents.ContainsKey(ContinueEvent))
            {
                HandleEvent(new Events.Event(ContinueEvent));
            }
        }
    }
}
