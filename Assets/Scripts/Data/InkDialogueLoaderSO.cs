using UnityEngine;

using Dialogue;

namespace Data
{
    [CreateAssetMenu(fileName="InkDialogueLoaderSO")]
    public class InkDialogueLoaderSO : DialogueLoaderSO
    {
        [SerializeField] private SaveManagerSO _saveManager;
        [SerializeField] private ConfigurationsManagerSO _configurationManager;

        [SerializeField] private string _dialogueFolder = "Dialogues";
        [SerializeField] private string _firstDialogueFile = "main_dialogue";
        [SerializeField] private string _languageConfiguration = "language";


        public override IDialogue LoadDialogue()
        {
            SaveData data = _saveManager.GetCurrentSaveData();

            if (data.DialogueState is null)
            {
                return LoadDialogue(_firstDialogueFile);
            }

            throw new System.NotImplementedException();
        }

        private IDialogue LoadDialogue(string dialogueName)
        {
            string language = _configurationManager.GetStringConfiguration(_languageConfiguration);
            string fullPath = System.IO.Path.Join(_dialogueFolder, $"{dialogueName}{language}");
            TextAsset text = Resources.Load<TextAsset>(fullPath);
            try
            {
                return new InkDialogue(text.text);
            }
            catch(System.Exception ex)
            {
                Debug.LogError($"Can't load dialogue for {dialogueName}: {ex}");
                return new NullDialogue();
            }
        }
    }
}
