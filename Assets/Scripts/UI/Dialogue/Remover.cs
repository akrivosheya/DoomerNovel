using UnityEngine;

using UI.Dialogue.Elements;

namespace UI.Dialogue
{
    public class Remover : IDialoguePresenterBehaviour
    {
        private readonly int ElementIdIndex = 0;
        
        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            if (parameters.Length <= ElementIdIndex)
            {
                Debug.LogError($"parameters doesn't have element id parameter: its length is {parameters.Length}");
                return;
            }
            
            if (!presenter.TryGetElement(parameters[ElementIdIndex], out DialogueUIElement element))
            {
                Debug.LogError($"can't get element {parameters[ElementIdIndex]}");
                return;
            }

            element.Parent.Remove(element.ID);
        }
    }
}
