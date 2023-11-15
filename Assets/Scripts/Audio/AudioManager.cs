using UnityEngine;

using Data;
using Configurations;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainAmbientSource;
        [SerializeField] private AudioSource _secondAmbientSource;
        [SerializeField] private AudioSource _soundSource;

        [SerializeField] private ConfigurationsManagerSO _configurationsManager;
        [SerializeField] private ConfigurationSubject _configurationSubject;

        [SerializeField] private string _ambientVolumeConfiguration = "music";
        [SerializeField] private string _soundVolumeConfiguration = "sound";

        [SerializeField] private int _minVolume = 0;
        [SerializeField] private int _maxVolume = 1;

        private float _ambientVolume;
        private float _soundVolume;


        void Awake()
        {
            _configurationSubject.AddListener(OnConfigurationEvent);
        }

        void OnDestroy()
        {
            _configurationSubject.RemoveListener(OnConfigurationEvent);
        }

        void Start()
        {
            OnConfigurationEvent();
        }

        private void OnConfigurationEvent()
        {
            float newAmbientVolume = _configurationsManager.GetFloatConfiguration(_ambientVolumeConfiguration);
            float newSoundVolume = _configurationsManager.GetFloatConfiguration(_soundVolumeConfiguration);

            newAmbientVolume = Mathf.Clamp(newAmbientVolume, _minVolume, _maxVolume);
            newSoundVolume = Mathf.Clamp(newSoundVolume, _minVolume, _maxVolume);

            if (newAmbientVolume != _ambientVolume)
            {
                _ambientVolume = newAmbientVolume;
                _mainAmbientSource.volume = _ambientVolume;
                _secondAmbientSource.volume = _ambientVolume;
            }

            if (newSoundVolume != _soundVolume)
            {
                _soundVolume = newSoundVolume;
                _soundSource.volume = _soundVolume;
            }
        }
    }
}
