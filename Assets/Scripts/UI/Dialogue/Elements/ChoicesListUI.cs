using UnityEngine;

using Factory;

namespace UI.Dialogue.Elements
{
    public class ChoicesListUI : CompositeDialogueUIElement
    {
        [SerializeField] private string _interactionWrapperId = "interaction";
        [SerializeField] private string _idDelim = "_";
        [SerializeField] private string _interactionEvent = "choice";
        [SerializeField] private float _choicesOffset = 50f;


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
                    _interactionEvent
                }
            };
            if (!ElementsFactory.TryGetObject(interactionInitialData, out DialogueUIElement interaction))
            {
                return;
            }

            interaction.SetChild(newChild);
            newChild.transform.localPosition = Vector3.zero;
            SetChild(interaction);

            float childsHalfCount = Childs.Count / 2;
            int changedChilds = 0;
            foreach (DialogueUIElement child in Childs)
            {
                child.transform.localPosition = new Vector3(0, (changedChilds - childsHalfCount) * _choicesOffset);
            }
        }
    }
}
