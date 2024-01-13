using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LanguageConfigurationUI : LanguageDependentUI, IConfigurationUI
    {
        public bool Changed { get => _languageIndex != _newLanguageIndex; }

        [SerializeField] private Dropdown _dropdown;
        [SerializeField] private Text _text;

        [SerializeField] private string _textType = "language";
        [SerializeField] private string _languageConfiguration = "language";

        private int _languageIndex;
        private int _newLanguageIndex;

        public override void SetActive(bool isActive)
        {
            _dropdown.gameObject.SetActive(isActive);
            _text.gameObject.SetActive(isActive);
            if (isActive)
            {
                _languageIndex = _dropdown.options.FindIndex(data => data.text == CurrentLanguage);
                _newLanguageIndex = _languageIndex;
                _dropdown.SetValueWithoutNotify(_languageIndex);
                if (HasLanguageMismatch)
                {
                    _text.text = GetText(_textType);
                }
            }
        }

        public void OnApplyConfiguration()
        {
            if (!Changed)
            {
                return;
            }

            _languageIndex = _newLanguageIndex;
            ConfigurationsManager.SetStringConfiguration(_languageConfiguration, _dropdown.options[_languageIndex].text);
        }

        public void OnChange(int languageIndex)
        {
            _newLanguageIndex = languageIndex;
        }

        protected override void UpdateLanguageDependentContext()
        {
            _text.text = GetText(_textType);
        }
    }
}
