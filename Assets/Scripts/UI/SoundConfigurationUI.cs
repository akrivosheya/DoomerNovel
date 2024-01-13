using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundConfigurationUI : LanguageDependentUI, IConfigurationUI
    {
        public bool Changed { get => _volume != _newVolume; }

        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;

        [SerializeField] private string _soundConfiguration;
        [SerializeField] private string _textType = "sound";

        private float _volume;
        private float _newVolume;


        public override void SetActive(bool isActive)
        {
            _slider.gameObject.SetActive(isActive);
            _text.gameObject.SetActive(isActive);
            if (isActive)
            {
                _volume = ConfigurationsManager.GetFloatConfiguration(_soundConfiguration);
                _slider.value = _volume;
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

            _volume = _newVolume;
            ConfigurationsManager.SetFloatConfiguration(_soundConfiguration, _volume);
        }

        public void OnChange(float volume)
        {
            _newVolume = volume;
        }

        protected override void UpdateLanguageDependentContext()
        {
            _text.text = GetText(_textType);
        }
    }
}
