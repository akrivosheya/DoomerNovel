using UnityEngine;

using Dialogue;
using System.Linq;

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
        [SerializeField] private char _metadataDelim = ';';


        public override void SaveDialogue(string sceneState, string dialogueState)
        {
            SaveData saveData = new SaveData(null, System.DateTime.Now.ToString(), dialogueState, sceneState);
            _saveManager.SaveDialogue(saveData);
        }

        public override IDialogue LoadDialogue()
        {
            IDialogue dialogue = LoadDialogue(_firstDialogueFile);
            SaveData data = _saveManager.GetCurrentSaveData();
            if (!data.IsEmpty && dialogue is InkDialogue inkDialogue)
            {
                inkDialogue.SetState(data.DialogueState);
                inkDialogue.SetMetadata(data.SceneState.Split(_metadataDelim).Select(metadata => metadata.Trim()).ToList());
                return inkDialogue;
            }


            return dialogue;
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
