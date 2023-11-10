using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Configurations;
using Data;

namespace UI
{
    public class MenuController : ConfigurationSubject
    {
        [SerializeField] private MainMenuUI _mainMenu;
        [SerializeField] private SettingsUI _settings;
        [SerializeField] private LanguageManagerSO _languageManager;


        void Start()
        {
            _languageManager.OnConfigurationEvent(true);
            OnConfigurationEvent();
            _mainMenu.SetActive(true);
            _settings.SetActive(false);

            _settings.AddListenerOnApplySettings(OnApplyConfiguration);
            _settings.AddListenerOnBackSettings(OnBackSettings);
        }

        void OnDestroy()
        {
            _settings.RemoveListenerOnApplySettings(OnApplyConfiguration);
            _settings.RemoveListenerOnBackSettings(OnBackSettings);
        }

        public void OnClickNewGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnClickSettings()
        {
            _mainMenu.SetActive(false);
            _settings.SetActive(true);
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
    }
}
