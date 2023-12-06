using UnityEngine;

using Factory;

namespace UI.Dialogue.Elements
{
    public class ChoicesListUI : CompositeDialogueUIElement
    {
        [SerializeField] private string _interactionWrapperId = "interaction";
        [SerializeField] private string _idDelim = "_";
        [SerializeField] private float _choicesOffset = 100f;

        private readonly Events.Event.EventTypes _beginChoiceEvent = Events.Event.EventTypes.BeginChoice;
        private readonly Events.Event.EventTypes _endChoiceEvent = Events.Event.EventTypes.EndChoice;
        private readonly string _idKey = "id";

        private bool _hasChoices = false;


        public override void SetChild(DialogueUIElement newChild)
        {
            InitialData interactionInitialData = new InitialData()
            {
                Type = _interactionWrapperId,
                Parameters = new string[]
                {
                    $"{newChild.ID}{_idDelim}{_interactionWrapperId}",
                    0.ToString(),
                    0.ToString(),
                    _endChoiceEvent.ToString()
                }
            };
            if (!ElementsFactory.TryGetObject(interactionInitialData, out DialogueUIElement interaction))
            {
                return;
            }

            interaction.SetChild(newChild);
            newChild.transform.localPosition = Vector3.zero;
            base.SetChild(interaction);

            float childsHalfCount = Childs.Count / 2;
            int changedChilds = Childs.Count;
            foreach (DialogueUIElement child in Childs)
            {
                child.transform.localPosition = new Vector3(0, (changedChilds - childsHalfCount) * _choicesOffset);
                changedChilds--;
            }

            if (!_hasChoices)
            {
                _hasChoices = true;
                Parent.HandleEvent(new Events.Event(_beginChoiceEvent));
            }
        }

        public override void HandleEvent(Events.Event currentEvent)
        {
            if (currentEvent.EventType != _beginChoiceEvent && currentEvent.EventType != _endChoiceEvent)
            {
                base.HandleEvent(currentEvent);
                return;
            }

            if (currentEvent.EventType == _beginChoiceEvent)
            {
                Debug.LogError($"try to begin choice in other choice list");
                return;
            }

            if (currentEvent.EventType == _endChoiceEvent && !_hasChoices)
            {
                Debug.LogError($"try to end choice not in choice time");
                return;
            }

            string wrapperId = currentEvent.GetStringValue(_idKey);
            int wrapperIndex = Childs.FindIndex((child) => child.ID == wrapperId);
            if (wrapperIndex < 0)
            {
                Debug.LogError($"can't make choice: {wrapperId} is not child of choices list");
                return;
            }

            Events.Event newEvent = new Events.Event(_endChoiceEvent);
            newEvent.SetIntValue(_idKey, wrapperIndex);
            Parent.HandleEvent(newEvent);
        }
    }
}
