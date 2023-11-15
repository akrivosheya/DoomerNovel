using System.Collections.Generic;
using UnityEngine;

using Dialogue;
using Factory;
using UI.Dialogue.Elements;

namespace UI.Dialogue
{
    public class DialoguePresenter : MonoBehaviour
    {

        private readonly string[] EmptyParameters = new string[0];

        [SerializeField] private DialogueManagerSO _dialogueManager;
        [SerializeField] private FactorySO<IDialoguePresenterBehaviour> _factory;
        [SerializeField] private DialogueUIRoot _dialogueRoot;


        public void StartGame()
        {
            _dialogueManager.LoadDialogue();
            _factory.Initialize();
            DoBehaviours();
        }

        public void AddElement(string elementType, string containerId, params string[] initParameters)
        {
            InitialData init = new InitialData() { Type=elementType, Parameters=initParameters };
            _dialogueRoot.AddElement(init, containerId);
        }

        private void DoBehaviours()
        {
            List<InitialData> initials = _dialogueManager.GetCommandsData();
            foreach (InitialData initial in initials)
            {
                InitialData initialForFactory = new InitialData()
                {
                    Type=initial.Type,
                    Parameters=EmptyParameters
                };
                
                if (_factory.TryGetObject(initialForFactory, out IDialoguePresenterBehaviour behaviour))
                {
                    behaviour.DoBehaviour(this, initial.Parameters);
                }
            }
        }
    }
}
