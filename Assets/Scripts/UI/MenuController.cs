using UnityEngine;

using Configurations;
using Data;
using UI.Dialogue;
using System;

namespace UI
{
    public class MenuController : ConfigurationSubject
    {
        [SerializeField] private MainMenuUI _mainMenu;
        [SerializeField] private SavesUI _menuSaves;
        [SerializeField] private SavesUI _gameSaves;
        [SerializeField] private SettingsUI _settings;
        [SerializeField] private DialoguePresenter _presenter;
        [SerializeField] private LanguageManagerSO _languageManager;


        void Start()
        {
            _languageManager.OnConfigurationEvent(true);
            OnConfigurationEvent();
            _mainMenu.SetActive(true);
            _settings.SetActive(false);
            _menuSaves.SetActive(false);
            _gameSaves.SetActive(false);

            _mainMenu.AddListenerOnNewGame(OnNewGame);
            _settings.AddListenerOnApplySettings(OnApplyConfiguration);
            _settings.AddListenerOnBackSettings(OnBackSettings);
            _menuSaves.AddListenerOnBackSaves(OnBackMenuSaves);
            _menuSaves.AddListenerOnContinue(OnContinue);
            _gameSaves.AddListenerOnBackSaves(OnBackGameSaves);
            _gameSaves.AddListenerOnSave(OnSave);
        }

        void OnDestroy()
        {
            _mainMenu.RemoveListenerOnNewGame(OnNewGame);
            _settings.RemoveListenerOnApplySettings(OnApplyConfiguration);
            _settings.RemoveListenerOnBackSettings(OnBackSettings);
            _menuSaves.RemoveListenerOnBackSaves(OnBackMenuSaves);
            _menuSaves.RemoveListenerOnContinue(OnContinue);
            _gameSaves.RemoveListenerOnBackSaves(OnBackGameSaves);
            _gameSaves.RemoveListenerOnSave(OnSave);
        }

        public void OnNewGame()
        {
            _mainMenu.SetActive(false);
            _presenter.StartGame();
        }

        public void OnFinishGame()
        {
            _mainMenu.SetActive(true);
        }

        public void OnClickSettings()
        {
            _mainMenu.SetActive(false);
            _settings.SetActive(true);
        }

        public void OnClickSaves()
        {
            _mainMenu.SetActive(false);
            _menuSaves.SetActive(true);
        }

        public void OnExitDialogue()
        {
            _mainMenu.OnExitDialogue(OnConfirmExitDialogue, OnDenyExitDialogue);
        }

        public void OnSaveDialogue()
        {
            _gameSaves.SetActive(true);
        }

        public void OnSave()
        {
            _presenter.SaveDialogue();
            _gameSaves.UpdateSelectedSave();
        }

        private void OnApplyConfiguration()
        {
            _languageManager.OnConfigurationEvent(false);
            OnConfigurationEvent();
        }

        private void OnBackSettings()
        {
            _mainMenu.SetActive(true);
            _settings.SetActive(false);
        }

        private void OnBackMenuSaves()
        {
            _mainMenu.SetActive(true);
            _menuSaves.SetActive(false);
        }

        private void OnBackGameSaves()
        {
            _gameSaves.SetActive(false);
            _presenter.ContinueDialogue();
        }

        private void OnContinue()
        {
            _menuSaves.SetActive(false);
            _presenter.StartGame();
        }

        private void OnConfirmExitDialogue()
        {
            _presenter.ExitDialogue();
        }

        private void OnDenyExitDialogue()
        {
            _presenter.UnpauseDialogue();
        }
    }
}
