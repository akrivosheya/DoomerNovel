using UnityEngine;

using Factory;

namespace UI.Dialogue.Elements
{
    public class DialogueUIRoot : CompositeDialogueUIElement
    {
        [SerializeField] private string _emptyId = "_";

        void Awake()
        {
            ElementsFactory.Initialize();
        }

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
    }
}
