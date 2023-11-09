using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName="InkDialogueLoader")]
    public class InkDialogueLoader : IDialogueLoader
    {
        [SerializeField] private readonly string _dialogueFile = "Dialogues";


        public override bool TryLoadDialogue(string objectName, string sceneName, out IDialogue dialogue)
        {
            string fullPath = System.IO.Path.Join(_dialogueFile, sceneName, objectName);
            TextAsset text = Resources.Load<TextAsset>(fullPath);
            try
            {
                dialogue = new InkDialogue(text.text);
                return true;
            }
            catch(System.Exception ex)
            {
                Debug.LogError("Can't load dialogue for " + objectName + " in " + sceneName + ": " + ex);
                dialogue = default;
                return false;
            }
        }
    }
}
