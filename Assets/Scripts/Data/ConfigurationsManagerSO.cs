using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="ConfigurationsManagerSO")]
    public class ConfigurationsManagerSO : ScriptableObject
    {
        private readonly string EmptyString = "";

        [SerializeField] float _defaultFloat = 0.5f;


        public string GetStringConfiguration(string key)
        {
            return (PlayerPrefs.HasKey(key)) ? PlayerPrefs.GetString(key) : EmptyString;
        }

        public float GetFloatConfiguration(string key)
        {
            return (PlayerPrefs.HasKey(key)) ? PlayerPrefs.GetFloat(key) : _defaultFloat;
        }

        public void SetStringConfiguration(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public void SetFloatConfiguration(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }
}
