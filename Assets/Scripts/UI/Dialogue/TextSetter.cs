using UnityEngine;

using UI.Dialogue.Elements;

namespace UI.Dialogue
{
    public class TextSetter : IDialoguePresenterBehaviour
    {
        private readonly int ElementIdIndex = 0;

        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            if (parameters.Length <= ElementIdIndex)
            {
                Debug.LogError($"parameters doesn't have element id parameter: its length is {parameters.Length}");
                return;
            }

            string text = presenter.GetText();
            if (!presenter.TryGetElement(parameters[ElementIdIndex], out DialogueUIElement element))
            {
                Debug.LogError($"can't get element {parameters[ElementIdIndex]}");
                return;
            }

            if (element is TextUI textElement)
            {
                textElement.SetText(text);
            }
            else
            {
                Debug.LogError($"{parameters[ElementIdIndex]} isn't TextUI element");
            }
        }
    }
}
