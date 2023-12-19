using UnityEngine;

using Dialogue;

namespace Data
{
    public abstract class DialogueLoaderSO : ScriptableObject
    {
        public abstract IDialogue LoadDialogue();
        public abstract void SaveDialogue(string sceneState, string dialogueState);
    }
}
