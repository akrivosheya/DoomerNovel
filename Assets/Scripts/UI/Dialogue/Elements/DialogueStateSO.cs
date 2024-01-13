using UnityEngine;

namespace UI.Dialogue.Elements
{
    public abstract class DialogueStateSO : ScriptableObject
    {
        public abstract void HandleEvent(Events.Event newEvent, Events.EventHandlersManager eventsManager);
        public abstract void StateLateUpdate(DialogueUIRoot dialogueRoot, Events.EventHandlersManager eventsManager);
        public abstract void StateUpdate(Events.EventHandlersManager eventsManager);
        
        public virtual void Reset() { }
    }
}
