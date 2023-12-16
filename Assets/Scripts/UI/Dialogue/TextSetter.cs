using UnityEngine;

using UI.Dialogue.Elements;

namespace UI.Dialogue
{
    public class TextSetter : IDialoguePresenterBehaviour
    {
        private readonly int ElementIdIndex = 0;
        private readonly int ChoiceIdIndex = 1;

        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            if (parameters.Length <= ElementIdIndex)
            {
                Debug.LogError($"parameters doesn't have element id parameter: its length is {parameters.Length}");
                return;
            }

            string text = "";
            if (parameters.Length > ChoiceIdIndex)
            {
                if (int.TryParse(parameters[ChoiceIdIndex], out int choiceId))
                {
                    text = presenter.GetChoiceText(choiceId);
                }
                else
                {
                    Debug.LogError($"{parameters[ChoiceIdIndex]} is not choice id");
                }
            }
            else
            {
                text = presenter.GetText();
            }

            if (!presenter.TryGetElement(parameters[ElementIdIndex], out DialogueUIElement element))
            {
                Debug.LogError($"can't get element {parameters[ElementIdIndex]}");
                return;
            }

            if (element is TextUI textElement)
            {
                textElement.SetText(text);
                textElement.StartWriting();
            }
            else
            {
                Debug.LogError($"{parameters[ElementIdIndex]} isn't TextUI element");
            }
        }
    }
}
