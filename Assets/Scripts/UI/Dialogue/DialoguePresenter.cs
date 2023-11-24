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

        [SerializeField] private MenuController _menuController;
        [SerializeField] private DialogueManagerSO _dialogueManager;
        [SerializeField] private FactorySO<IDialoguePresenterBehaviour> _factory;
        [SerializeField] private DialogueUIRoot _dialogueRoot;


        void Awake()
        {
            _factory.Initialize();
            _dialogueRoot.SetHandler(Events.Event.EventTypes.Continue, (currentEvent) => Next());
        }

        public void StartGame()
        {
            _dialogueManager.LoadDialogue();
            _dialogueRoot.Reset();
            DoBehaviours();
        }

        public void Next()
        {
            if (_dialogueManager.CanContinue)
            {
                _dialogueManager.Continue();
                DoBehaviours();
            }
            else
            {
                _dialogueRoot.Clear();
                _dialogueRoot.Pause();
                _menuController.OnFinishGame();
            }
        }

        public string GetText() => _dialogueManager.GetText();

        public void AddElement(string elementType, string containerId, params string[] initParameters)
        {
            InitialData init = new InitialData() { Type=elementType, Parameters=initParameters };
            _dialogueRoot.AddElement(init, containerId);
        }

        public bool TryGetElement(string id, out DialogueUIElement element) => _dialogueRoot.TryGetChild(id, out element);

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
