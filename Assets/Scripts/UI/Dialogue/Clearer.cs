using UnityEngine;

using UI.Dialogue.Elements;

namespace UI.Dialogue
{
    public class Clearer : IDialoguePresenterBehaviour
    {
        private readonly int _elementIdIndex = 0;

        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            if (parameters.Length <= _elementIdIndex)
            {
                Debug.LogError($"parameters doesn't have element id parameter: its length is {parameters.Length}");
                return;
            }

            if (!presenter.TryGetElement(parameters[_elementIdIndex], out DialogueUIElement element))
            {
                Debug.LogError($"can't get element {parameters[_elementIdIndex]}");
                return;
            }

            element.Clear();
        }
    }
}
