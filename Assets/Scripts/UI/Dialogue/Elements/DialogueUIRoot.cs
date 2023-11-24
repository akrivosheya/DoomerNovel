using System;
using System.Collections.Generic;
using UnityEngine;

using Factory;
using Events = UI.Dialogue.Events;

namespace UI.Dialogue.Elements
{
    public class DialogueUIRoot : CompositeDialogueUIElement
    {
        [SerializeField] private DialogueStateSO _currentState;
        [SerializeField] private DialogueStateSO _firstState;
        [SerializeField] private DialogueStateSO _pauseState;
        [SerializeField] private string _emptyId = "_";

        private Dictionary<Events.Event.EventTypes, Action<Events.Event>> _handlers = new Dictionary<Events.Event.EventTypes, Action<Events.Event>>();


        void Awake() => ElementsFactory.Initialize();
        void Update() => _currentState.StateUpdate();
        void LateUpdate() => _currentState.StateLateUpdate(_handlers, this);

        public void AddElement(InitialData initialData, string containerId)
        {
            if (!ElementsFactory.TryGetObject(initialData, out DialogueUIElement newElement))
            {
                return;
            }

            if (containerId == _emptyId)
            {
                SetChild(newElement);
            }
            else if (TryGetChild(containerId, out DialogueUIElement container))
            {
                container.SetChild(newElement);
            }
        }

        public void Reset() => _currentState = _firstState;
        public void Pause() => _currentState = _pauseState;
        public void SetHandler(Events.Event.EventTypes eventType, Action<Events.Event> handler) => _handlers.Add(eventType, handler);
    }
}
