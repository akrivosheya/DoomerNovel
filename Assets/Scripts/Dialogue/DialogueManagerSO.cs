using System.Collections.Generic;
using UnityEngine;

using Factory;
using Data;

namespace Dialogue
{
    [CreateAssetMenu(fileName="DialogueManagerSO")]
    public class DialogueManagerSO : ScriptableObject
    {
        public bool CanContinue { get { return !(_currentDialogue is null || !_currentDialogue.CanContinue); } }

        [SerializeField] private DialogueLoaderSO _loader;
        [SerializeField] private int _minElementsCount = 2;
        [SerializeField] private int _typeIndex = 0;
        [SerializeField] private int _parametersIndex = 1;
        [SerializeField] private char _elementsDelim = ':';
        [SerializeField] private char _parametersDelim = ',';

        private IDialogue _currentDialogue;
        private readonly string _emptyString = "";


        public void SaveDialogue(string sceneState)
        {
            _loader.SaveDialogue(sceneState, _currentDialogue.GetDialogueState());
        }

        public void LoadDialogue()
        {
            _currentDialogue = _loader.LoadDialogue();
        }

        public void Continue()
        {
            if(_currentDialogue != null && _currentDialogue.CanContinue)
            {
                _currentDialogue.Continue();
            }
        }

        public string GetText()
        {
            if(_currentDialogue != null)
            {
                return _currentDialogue.GetText();
            }
            else
            {
                return _emptyString;
            }
        }

        public string GetChoiceText(int choiceId)
        {
            if (!_currentDialogue.HasChoices)
            {
                return _emptyString;
            }

            return _currentDialogue.GetChoiceText(choiceId);
        }

        public void MakeChoice(int choiceId)
        {
            if (choiceId < 0)
            {
                Debug.LogError($"choice index must be integet more than 0");
                return;
            }

            _currentDialogue.MakeChoice(choiceId);
        }

        public List<InitialData> GetCommandsData()
        {
            var _initialList = new List<InitialData>();
            
            if(_currentDialogue != null)
            {
                var metadataList = _currentDialogue.GetMetadata();
                foreach(string metadata in metadataList)
                {
                    var elements = metadata.Split(_elementsDelim);
                    if(elements.Length < _minElementsCount)
                    {
                        continue;
                    }
                    var type = elements[_typeIndex];
                    var parameters = elements[_parametersIndex].Split(_parametersDelim);
                    _initialList.Add(new InitialData(){ Type=type, Parameters=parameters });
                }
            }

            return _initialList;
        }
    }
}
