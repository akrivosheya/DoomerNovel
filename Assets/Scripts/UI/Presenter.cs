using UnityEngine;

using Dialogue;

namespace UI
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private DialogueManager _dialogueManager;
        [SerializeField] private DialogueUI _dialogueUI;


        public void Activate(string objectName, string sceneName)
        {
            _dialogueUI.Activate();
            _dialogueManager.InitializeDialogue(objectName, sceneName);
        }
    }
}
