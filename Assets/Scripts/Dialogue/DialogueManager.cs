using System.Collections.Generic;
using UnityEngine;

using Factory;

namespace Dialogue
{
    [CreateAssetMenu(fileName="DialogueManager")]
    public class DialogueManager : ScriptableObject
    {
        public bool CanContinue { get { return !(_currentDialogue is null || !_currentDialogue.CanContinue); } }

        [SerializeField] private IDialogueLoader _loader;
        [SerializeField] private int _minElementsCount = 2;
        [SerializeField] private int _typeIndex = 0;
        [SerializeField] private int _parametersIndex = 0;
        [SerializeField] private char _elementsDelim = ':';
        [SerializeField] private char _parametersDelim = ',';

        private IDialogue _currentDialogue;
        private string _currentObject;
        private string _currentScene;
        private readonly string _emptyString = "";


        public void InitializeDialogue(string objectName, string sceneName)
        {
            if(_currentObject == objectName && _currentScene == sceneName)
            {
                _currentDialogue.Reset();
            }
            if(_loader.TryLoadDialogue(objectName, sceneName, out IDialogue dialogue))
            {
                _currentDialogue = dialogue;
            }
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
