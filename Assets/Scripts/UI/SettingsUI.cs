using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class SettingsUI : MenuElementsContainerUI
    {
        [SerializeField] private SoundConfigurationUI _soundConfiguration;
        [SerializeField] private SoundConfigurationUI _musicConfiguration;
        [SerializeField] private LanguageConfigurationUI _languageConfiguration;

        [SerializeField] private string _confirmMessage = "confirmSettings";
        [SerializeField] private string _backMessage = "backSettings";

        private UnityEvent _onApplySettings = new UnityEvent();
        private UnityEvent _onBackSettings = new UnityEvent();
        private List<IConfigurationUI> _configurations = new List<IConfigurationUI>();
        private List<IActivatingUIElement> _activatingUIs = new List<IActivatingUIElement>();


        public void AddListenerOnApplySettings(UnityAction onApplyAction)
        {
            _onApplySettings.RemoveAllListeners();
            _onApplySettings.AddListener(onApplyAction);
        }

        public void RemoveListenerOnApplySettings(UnityAction onApplyAction)
        {
            _onApplySettings.RemoveListener(onApplyAction);
        }
        
        public void AddListenerOnBackSettings(UnityAction onBackAction)
        {
            _onBackSettings.RemoveAllListeners();
            _onBackSettings.AddListener(onBackAction);
        }

        public void RemoveListenerOnBackSettings(UnityAction onBackAction)
        {
            _onBackSettings.RemoveListener(onBackAction);
        }

        public void OnApplySettings()
        {
            if (!_configurations.Any(configuration => configuration.Changed))
            {
                return;
            }

            ShowConfirmWindow(_confirmMessage, OnConfirmSettings, OnDenySettings);
        }

        public void OnBackSettings()
        {
            if (!_configurations.Any(configuration => configuration.Changed))
            {
                _onBackSettings.Invoke();
                return;
            }

            ShowConfirmWindow(_backMessage, OnConfirmBack, OnDenyBack);
        }

        protected override void SetActiveOtherElements(bool areActive)
        {
            foreach (IActivatingUIElement element in _activatingUIs)
            {
                element.SetActive(areActive);
            }
        }

        protected override void SetOtherElementsInterfaces()
        {
            _configurations.Add(_soundConfiguration);
            _configurations.Add(_musicConfiguration);
            _configurations.Add(_languageConfiguration);
            
            _activatingUIs.Add(_soundConfiguration);
            _activatingUIs.Add(_musicConfiguration);
            _activatingUIs.Add(_languageConfiguration);
        }
        
        private void OnConfirmSettings()
        {
            DestroyWindow();

            foreach (IConfigurationUI configuration in _configurations)
            {
                configuration.OnApplyConfiguration();
            }
            _onApplySettings.Invoke();
        }
        
        private void OnDenySettings()
        {
            DestroyWindow();
        }
        
        private void OnConfirmBack()
        {
            DestroyWindow();
            _onBackSettings.Invoke();
        }
        
        private void OnDenyBack()
        {
            DestroyWindow();
        }
    }
}