using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue.Elements
{
    [CreateAssetMenu(fileName="PauseStateSO")]
    public class PauseStateSO : DialogueStateSO
    {
        public override void HandleEvent(Events.Event newEvent) { }

        public override void StateLateUpdate(Dictionary<Events.Event.EventTypes, Action<Events.Event>> handlers, DialogueUIElement dialogueRoot) { }

        public override void StateUpdate() {}
    }
}
