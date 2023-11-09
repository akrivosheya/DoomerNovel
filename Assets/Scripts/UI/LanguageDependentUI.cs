using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Configurations;
using Data;

namespace UI
{
    public class LanguageDependentUI : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        [SerializeField] private Text _buttonText;

        [SerializeField] private ConfigurationsManagerSO _configurationsManager;
        [SerializeField] private ConfigurationSubject _configurationSubject;
        [SerializeField] private LanguageManagerSO _languageManager;

        [SerializeField] private string _stringsConfigurationKey = "language";
        [SerializeField] private string _buttonType;

        private string _currentLanguage;
        private bool _hasToChangeText = true;


        void Awake()
        {
            _configurationSubject.AddListener(OnConfigurationEvent);
        }

        void OnDestroy()
        {
            _configurationSubject.RemoveListener(OnConfigurationEvent);
        }

        void SetActive(bool isActive)
        {
            _button.SetActive(isActive);

            if (isActive && _hasToChangeText)
            {
                _buttonText.text = _languageManager.GetText(_buttonType);
            }
        }

        public void OnConfigurationEvent()
        {
            string newLanguage = _configurationsManager.GetStringConfiguration(_stringsConfigurationKey);
            if (_currentLanguage == newLanguage)
            {
                return;
            }

            _currentLanguage = newLanguage;
            _hasToChangeText = true;
        }
    }
}
