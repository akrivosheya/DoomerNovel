using UnityEngine;

using Data;
using Configurations;
using System.Collections;

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
        [SerializeField] private string _clipsFolder = "Audio";

        [SerializeField] private float _musicChangingSpeed = 0.5f;
        [SerializeField] private int _minVolume = 0;
        [SerializeField] private int _maxVolume = 1;

        private float _ambientVolume;
        private float _soundVolume;
        private bool _musicIsChanging = false;


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

        public void PlaySound(string clipName)
        {
            if (!TryGetClip(clipName, out AudioClip clip))
            {
                Debug.Log($"Can't find clip {clipName}");
                return;
            }

            _soundSource.PlayOneShot(clip);
        }

        public void PlayMusic(string clipName)
        {
            if (_musicIsChanging)
            {
                return;
            }

            if (!TryGetClip(clipName, out AudioClip clip))
            {
                Debug.Log($"Can't find clip {clipName}");
                return;
            }

            StartCoroutine(PlayMusic(clip));
        }

        private IEnumerator PlayMusic(AudioClip clip)
        {
            _musicIsChanging = true;
            float finalVolume = _ambientVolume;
            _secondAmbientSource.clip = clip;
            _secondAmbientSource.Play();

            while (finalVolume > _secondAmbientSource.volume)
            {
                yield return null;
                _secondAmbientSource.volume += _musicChangingSpeed * Time.deltaTime;
                _mainAmbientSource.volume = finalVolume - _secondAmbientSource.volume;
            }

            _secondAmbientSource.volume = finalVolume;
            _mainAmbientSource.volume = 0;
            _mainAmbientSource.Stop();
            AudioSource tmp = _secondAmbientSource;
            _secondAmbientSource = _mainAmbientSource;
            _mainAmbientSource = tmp;
            _musicIsChanging = false;
        }

        private bool TryGetClip(string clipName, out AudioClip clip)
        {
            clip = Resources.Load<AudioClip>(System.IO.Path.Join(_clipsFolder, clipName));
            return true;
        }
    }
}
