using System;
using System.Collections.Generic;

namespace UI.Dialogue.Events
{
    public class EventHandlersManager
    {

        private Dictionary<Event.EventTypes, Action<Event>> _handlers = new Dictionary<Event.EventTypes, Action<Event>>();
        private Dictionary<Event.EventTypes, Event> _currentEvents = new Dictionary<Event.EventTypes, Event>();
        

        public void AddEvent(Event.EventTypes eventType, Event newEvent) => _currentEvents.Add(eventType, newEvent);
        public void SetHandler(Event.EventTypes eventType, Action<Event> handler) => _handlers.Add(eventType, handler);
        public bool CanHandleEvent(Event.EventTypes eventType) => _currentEvents.ContainsKey(eventType) && _handlers.ContainsKey(eventType);
        public bool RegisteredEvent(Event.EventTypes eventType) => _currentEvents.ContainsKey(eventType);
        public void ClearEvents() => _currentEvents.Clear();
        public void RemoveEvent(Event.EventTypes eventType) => _currentEvents.Remove(eventType);
        public void HandleEvent(Event.EventTypes eventType)
        {
            if (_handlers.ContainsKey(eventType) && _currentEvents.ContainsKey(eventType))
            {
                _handlers[eventType](_currentEvents[eventType]);
            }
        }
    }
}
