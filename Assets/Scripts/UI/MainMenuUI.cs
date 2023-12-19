using UnityEngine;
using UnityEngine.Events;

using Data;

namespace UI
{
    public class MainMenuUI : MenuElementsContainerUI
    {
        [SerializeField] private SaveManagerSO _saveManager;

        [SerializeField] private string _exitGameMessage = "exitMessage";
        [SerializeField] private string _exitDialogueMessage = "exitDialogueMessage";
        [SerializeField] private string _newGameMessage = "newGameMessage";

        private UnityEvent _onNewGame = new UnityEvent();


        public void AddListenerOnNewGame(UnityAction onNewGameAction)
        {
            _onNewGame.RemoveAllListeners();
            _onNewGame.AddListener(onNewGameAction);
        }

        public void RemoveListenerOnNewGame(UnityAction onNewGameAction)
        {
            _onNewGame.RemoveListener(onNewGameAction);
        }

        public void OnNewGame()
        {
            ShowConfirmWindow(_newGameMessage, OnConfirmNewGame, DestroyWindow);
        }

        public void OnExitDialogue(UnityAction onConfirmAction, UnityAction onDenyAction)
        {
            ShowConfirmWindow(_exitDialogueMessage, () => 
            {
                DestroyWindow();
                onConfirmAction();
            }, () =>
            {
                DestroyWindow();
                onDenyAction();
            });
        }

        public void OnExitGame()
        {
            ShowConfirmWindow(_exitGameMessage, OnConfirmExit, DestroyWindow);
        }

        private void OnConfirmExit()
        {
            Application.Quit();
        }

        private void OnConfirmNewGame()
        {
            DestroyWindow();
            _saveManager.ResetSaveData();
            _onNewGame.Invoke();
        }
    }
}
