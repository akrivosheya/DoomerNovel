using UnityEngine;

namespace Dialogue
{
    public abstract class IDialogueLoader : ScriptableObject
    {
        public abstract bool TryLoadDialogue(string objectName, string sceneName, out IDialogue dialogue);
    }
}
