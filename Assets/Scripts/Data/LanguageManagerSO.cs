using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="LanguageManagerSO")]
    public class LanguageManagerSO : ScriptableObject
    {
        public string CurrentLanguage { get; private set; } = "";

        private readonly string EmptyString = "";
        private readonly int KeyValueSize = 2;

        [SerializeField] private ConfigurationsManagerSO _configurationsManager;

        [SerializeField] private string _stringsFolder = "Text";
        [SerializeField] private string _stringsFile = "Strings";
        [SerializeField] private string _keyValueSeparator = ":";
        [SerializeField] private string _stringsSeparator = ";";
        [SerializeField] private string _spaceReplacement = "_";
        [SerializeField] private string _space = " ";
        [SerializeField] private string _stringsConfigurationKey = "language";
        [SerializeField] private string _defaultLanguage = "Rus";

        [SerializeField] private int _keyIndex = 0;
        [SerializeField] private int _valueIndex = 1;

        private Dictionary<string, string> _strings = new Dictionary<string, string>();


        public string GetText(string key)
        {
            return _strings.GetValueOrDefault<string, string>(key);
        }

        public void OnConfigurationEvent(bool firstInitialization)
        {
            string newLanguage = _configurationsManager.GetStringConfiguration(_stringsConfigurationKey);
            newLanguage = (newLanguage == EmptyString) ? _defaultLanguage : newLanguage;
            if (!firstInitialization && CurrentLanguage == newLanguage)
            {
                return;
            }

            CurrentLanguage = newLanguage;
            _strings.Clear();
            TextAsset stringsConfiguration = Resources.Load<TextAsset>($"{_stringsFolder}{System.IO.Path.DirectorySeparatorChar}{_stringsFile}{CurrentLanguage}");
            if (stringsConfiguration is null)
            {
                Debug.LogError($"Can't get data from file {_stringsFolder}{System.IO.Path.DirectorySeparatorChar}{_stringsFile}{CurrentLanguage}");
                return;
            }

            string[] strings = stringsConfiguration.text.Split(_stringsSeparator);
            foreach (string keyValue in strings)
            {
                string[] separatedKeyValue = keyValue.Trim().Replace(_spaceReplacement, _space).Split(_keyValueSeparator);
                if (separatedKeyValue.Length < KeyValueSize)
                {
                    continue;
                }
                _strings.Add(separatedKeyValue[_keyIndex], separatedKeyValue[_valueIndex]);
            }
        }
    }
}
