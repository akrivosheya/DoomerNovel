using System;
using System.Collections.Generic;
using UnityEngine;

using Factory;

namespace UI.Dialogue.Elements
{
    public class DialogueUIRoot : CompositeDialogueUIElement
    {
        [SerializeField] private DialogueStateSO _currentState;
        [SerializeField] private DialogueStateSO _firstState;
        [SerializeField] private DialogueStateSO _pauseState;
        [SerializeField] private DialogueStateSO _choiceState;
        [SerializeField] private string _emptyId = "_";

        private readonly Events.EventHandlersManager _eventsManager = new Events.EventHandlersManager();


        void Awake() => ElementsFactory.Initialize();
        void Update() => _currentState.StateUpdate(_eventsManager);
        void LateUpdate() => _currentState.StateLateUpdate(this, _eventsManager);

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

        public void Reset()
        {
            _eventsManager.ClearEvents();
            _firstState.Reset();
            _pauseState.Reset();
            _choiceState.Reset();
            _currentState = _firstState;
        }

        public override void HandleEvent(Events.Event currentEvent) => _currentState.HandleEvent(currentEvent, _eventsManager);
        public void Pause() => _currentState = _pauseState;
        public void BeginChoice() => _currentState = _choiceState;

        public void SetHandler(Events.Event.EventTypes eventType, Action<Events.Event> handler) => _eventsManager.SetHandler(eventType, handler);
    }
}
