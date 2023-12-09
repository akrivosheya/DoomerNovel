using System.Collections.Generic;
using UnityEngine;

using Dialogue;
using Factory;
using UI.Dialogue.Elements;
using PauseSystem;

namespace UI.Dialogue
{
    public class DialoguePresenter : MonoBehaviour
    {

        private readonly string[] EmptyParameters = new string[0];

        [SerializeField] private MenuController _menuController;
        [SerializeField] private DialogueManagerSO _dialogueManager;
        [SerializeField] private FactorySO<IDialoguePresenterBehaviour> _factory;
        [SerializeField] private DialogueUIRoot _dialogueRoot;
        [SerializeField] private PauseManager _pauseManager;

        private readonly string _idKey = "id";


        void Awake()
        {
            _factory.Initialize();
            _dialogueRoot.SetHandler(Events.Event.EventTypes.Continue, (currentEvent) => Next());
            _dialogueRoot.SetHandler(Events.Event.EventTypes.BeginChoice, (currentEvent) => _dialogueRoot.BeginChoice());
            _dialogueRoot.SetHandler(Events.Event.EventTypes.SpeedUp, (currentEvent) => _dialogueRoot.SpeedUp());
            _dialogueRoot.SetHandler(Events.Event.EventTypes.SpeedDown, (currentEvent) => _dialogueRoot.Reset());
            _dialogueRoot.SetHandler(Events.Event.EventTypes.EndChoice, (currentEvent) =>
            {
                if (currentEvent.HasIntValue(_idKey))
                {
                    int choiceIndex = currentEvent.GetIntValue(_idKey);
                    _dialogueManager.MakeChoice(choiceIndex);
                    _dialogueRoot.Reset();
                    DoBehaviours();
                }
            });
            _dialogueRoot.SetHandler(Events.Event.EventTypes.Exit, (currentEvent) =>
            {
                PauseDialogue();
                _menuController.OnExitDialogue();
            });
        }

        public void StartGame()
        {
            _dialogueManager.LoadDialogue();
            _dialogueRoot.Reset();
            DoBehaviours();
        }

        public void ExitDialogue()
        {
            _dialogueRoot.Clear();
            _dialogueRoot.Pause();
            _menuController.OnFinishGame();
        }

        public void UnpauseDialogue()
        {
            _dialogueRoot.Unpause();
            _pauseManager.Unpause();
        }

        public string GetText() => _dialogueManager.GetText();

        public string GetChoiceText(int choiceId) => _dialogueManager.GetChoiceText(choiceId);

        public void AddElement(string elementType, string containerId, params string[] initParameters)
        {
            InitialData init = new InitialData() { Type=elementType, Parameters=initParameters };
            _dialogueRoot.AddElement(init, containerId);
        }

        public bool TryGetElement(string id, out DialogueUIElement element) => _dialogueRoot.TryGetChild(id, out element);

        private void Next()
        {
            if (_dialogueManager.CanContinue)
            {
                _dialogueManager.Continue();
                DoBehaviours();
            }
            else
            {
                ExitDialogue();
            }
        }

        private void PauseDialogue()
        {
            _pauseManager.Pause();
            _dialogueRoot.Pause();
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
