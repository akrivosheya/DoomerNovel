using UnityEngine;

using Dialogue;

namespace Data
{
    public abstract class DialogueLoaderSO : ScriptableObject
    {
        public abstract IDialogue LoadDialogue();
    }
}
