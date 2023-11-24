using System;
using System.Collections.Generic;
using UnityEngine;

using Events = UI.Dialogue.Events;

namespace UI.Dialogue.Elements
{
    public abstract class DialogueStateSO : ScriptableObject
    {
        public abstract void HandleEvent(Events.Event newEvent);
        public abstract void StateLateUpdate(Dictionary<Events.Event.EventTypes, Action<Events.Event>> handlers, DialogueUIElement dialogueRoot);
        public abstract void StateUpdate();
    }
}
