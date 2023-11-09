using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="ConfigurationsManagerSO")]
    public class ConfigurationsManagerSO : ScriptableObject
    {
        [SerializeField] private List<string> _stringConfigurationsKeys = new List<string>();
        [SerializeField] private List<string> _floatConfigurationsKeys = new List<string>();

        private Dictionary<string, string> _stringConfigurations = new Dictionary<string, string>();
        private Dictionary<string, float> _floatConfigurations = new Dictionary<string, float>();


        void Awake()
        {
            foreach (string stringConfigurationKey in _stringConfigurationsKeys)
            {
                _stringConfigurations.Add(stringConfigurationKey, PlayerPrefs.GetString(stringConfigurationKey));
            }
            
            foreach (string floatConfigurationKey in _floatConfigurationsKeys)
            {
                _floatConfigurations.Add(floatConfigurationKey, PlayerPrefs.GetFloat(floatConfigurationKey));
            }
        }

        public string GetStringConfiguration(string key)
        {
            return _stringConfigurations.GetValueOrDefault<string, string>(key);
        }

        public float GetFloatConfiguration(string key)
        {
            return _floatConfigurations.GetValueOrDefault<string, float>(key);
        }

        public void SetStringConfiguration(string key, string value)
        {
            if (!_stringConfigurations.ContainsKey(key))
            {
                Debug.LogError($"Tried to set string configuration {key}, that is not exist!");
                return;
            }

            _stringConfigurations.Add(key, value);
            PlayerPrefs.SetString(key, value);
        }

        public void SetFloatConfiguration(string key, float value)
        {
            if (!_floatConfigurations.ContainsKey(key))
            {
                Debug.LogError($"Tried to set float configuration {key}, that is not exist!");
                return;
            }

            _floatConfigurations.Add(key, value);
            PlayerPrefs.SetFloat(key, value);
        }
    }
}
