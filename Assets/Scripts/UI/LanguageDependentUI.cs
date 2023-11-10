using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Configurations;
using Data;

namespace UI
{
    public abstract class LanguageDependentUI : MonoBehaviour, IActivatingUIElement
    {
        protected ConfigurationsManagerSO ConfigurationsManager { get => _configurationsManager; }
        protected string CurrentLanguage { get => _languageManager.CurrentLanguage; }
        protected bool HasLanguageMismatch { get => _isNotNotified; }

        [SerializeField] private ConfigurationSubject _configurationSubject;
        [SerializeField] private ConfigurationsManagerSO _configurationsManager;
        [SerializeField] private LanguageManagerSO _languageManager;

        private string _lastLanguage = "";
        private bool _isNotNotified = false;


        void Awake()
        {
            _configurationSubject.AddListener(OnConfigurationEvent);
        }

        void OnDestroy()
        {
            _configurationSubject.RemoveListener(OnConfigurationEvent);
        }

        public void OnConfigurationEvent()
        {
            if (CurrentLanguage == _lastLanguage)
            {
                return;
            }

            _lastLanguage = CurrentLanguage;
            _isNotNotified = true;

            UpdateLanguageDependentContext();
        }

        public abstract void SetActive(bool isActive);

        protected virtual void UpdateLanguageDependentContext()
        {

        }

        protected string GetText(string key)
        {
            _isNotNotified = false;
            return _languageManager.GetText(key);
        }
    }
}
