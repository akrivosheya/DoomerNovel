using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Configurations;

namespace Data
{
    [CreateAssetMenu(fileName="LanguageManagerSO")]
    public class LanguageManagerSO : ScriptableObject
    {
        private readonly int KeyValueSize = 2;

        [SerializeField] private ConfigurationsManagerSO _configurationsManager;
        [SerializeField] private ConfigurationSubject _configurationSubject;

        [SerializeField] private string _stringsFolder = "Text";
        [SerializeField] private string _stringsFile = "Strings";
        [SerializeField] private string _keyValueSeparator = ":";
        [SerializeField] private string _stringsSeparator = ";";
        [SerializeField] private string _stringsConfigurationKey = "language";

        [SerializeField] private int _keyIndex = 0;
        [SerializeField] private int _valueIndex = 1;

        private Dictionary<string, string> _strings = new Dictionary<string, string>();
        private string _currentLanguage;


        void Awake()
        {
            _configurationSubject.AddListener(OnConfigurationEvent);
            InitializeStrings();
        }

        void OnDestroy()
        {
            _configurationSubject.RemoveListener(OnConfigurationEvent);
        }

        public string GetText(string key)
        {
            return _strings.GetValueOrDefault<string, string>(key);
        }

        private void OnConfigurationEvent()
        {
            InitializeStrings();
        }

        private void InitializeStrings()
        {
            string newLanguage = _configurationsManager.GetStringConfiguration(_stringsConfigurationKey);
            if (_currentLanguage == newLanguage)
            {
                return;
            }

            _currentLanguage = newLanguage;
            _strings.Clear();
            TextAsset stringsConfiguration = Resources.Load<TextAsset>(System.IO.Path.Combine(_stringsFolder, _stringsFile));
            if (stringsConfiguration is null)
            {
                Debug.LogError($"Can't get data from file {System.IO.Path.Combine(_stringsFolder, $"{_stringsFile}{_currentLanguage}")}");
                return;
            }

            string[] strings = stringsConfiguration.text.Split(_stringsSeparator);
            foreach (string keyValue in strings)
            {
                string[] separatedKeyValue = keyValue.Split(_keyValueSeparator);
                if (separatedKeyValue.Length < KeyValueSize)
                {
                    continue;
                }
                _strings.Add(separatedKeyValue[_keyIndex], separatedKeyValue[_valueIndex]);
            }
        }
    }
}
