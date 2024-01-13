using UnityEngine;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="PauseStateSO")]
    public class PauseStateSO : DialogueStateSO
    {
        public override void HandleEvent(Events.Event newEvent, Events.EventHandlersManager eventsManager) { }

        public override void StateLateUpdate(DialogueUIRoot dialogueRoot, Events.EventHandlersManager eventsManager) { }

        public override void StateUpdate(Events.EventHandlersManager eventsManager) {}
    }
}
