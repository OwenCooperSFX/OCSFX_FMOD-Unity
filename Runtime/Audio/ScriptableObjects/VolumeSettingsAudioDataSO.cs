using System;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using OCSFX.UI;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/VolumeSettings Audio Data")]
    public class VolumeSettingsAudioDataSO : AudioDataSO
    {
        [Header("Parameters")]
        [SerializeField] [ParamRef] private string _masterVolumeParam;
        [SerializeField] [ParamRef] private string _musicVolumeParam;
        [SerializeField] [ParamRef] private string _sfxVolumeParam;
        [SerializeField] [ParamRef] private string _uiVolumeParam;
        [SerializeField] [ParamRef] private string _ambVolumeParam;
        [SerializeField] [ParamRef] private string _voiceVolumeParam;

        [Header("Values")]
        [SerializeField] [Range(0,1)] private float _masterVolume = 1;
        [SerializeField] [Range(0,1)] private float _musicVolume = 1;
        [SerializeField] [Range(0,1)] private float _sfxVolume = 1;
        [SerializeField] [Range(0,1)] private float _uiVolume = 1;
        [SerializeField] [Range(0,1)] private float _ambVolume = 1;
        [SerializeField] [Range(0,1)] private float _voiceVolume = 1;

        [Header("PlayerPrefs")]
        private PlayerPrefsData _playerPrefsData;

        private const float _defaultValue = 1.0f;
        private const float _defaultMasterValue = 0.9f;

        private void OnEnable()
        {
            MasterBanksLoaded += OnMasterBanksLoaded;
        }
        
        private void OnDisable()
        {
            MasterBanksLoaded -= OnMasterBanksLoaded;
        }

        private void OnMasterBanksLoaded() => LoadPlayerPrefs();

        private void LoadPlayerPrefs()
        {
            SetVolume(AudioVolumeSetting.Master, _playerPrefsData.MasterValue);
            SetVolume(AudioVolumeSetting.Music, _playerPrefsData.MusicValue);
            SetVolume(AudioVolumeSetting.Sfx, _playerPrefsData.SfxValue);
            SetVolume(AudioVolumeSetting.Ui, _playerPrefsData.UiValue);
            SetVolume(AudioVolumeSetting.Amb, _playerPrefsData.AmbValue);
            SetVolume(AudioVolumeSetting.Voice, _playerPrefsData.VoiceValue);
        }

        public void SetMasterVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Master, slider.value);

            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);

            _playerPrefsData.Master = sliderPlayerPrefHandler.PlayerPrefName;
        }
        
        public void SetMusicVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Music, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);

            _playerPrefsData.Music = sliderPlayerPrefHandler.PlayerPrefName;
            
        }

        public void SetSfxVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Sfx, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);

            _playerPrefsData.Sfx = sliderPlayerPrefHandler.PlayerPrefName;
        }

        public void SetUiVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Ui, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);

            _playerPrefsData.Ui = sliderPlayerPrefHandler.PlayerPrefName;
        }

        public void SetAmbVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Amb, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);

            _playerPrefsData.Amb = sliderPlayerPrefHandler.PlayerPrefName;
        }
        
        public void SetVoiceVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Voice, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);

            _playerPrefsData.Voice = sliderPlayerPrefHandler.PlayerPrefName;
        }


        private void SetVolume(AudioVolumeSetting setting, float newValue)
        {
            string paramRef = null;

            newValue = Mathf.Clamp(newValue, 0, 1);

            switch (setting)
            {
                case AudioVolumeSetting.Master:
                    paramRef = _masterVolumeParam;
                    _masterVolume = newValue;
                    break;
                case AudioVolumeSetting.Music:
                    paramRef = _musicVolumeParam;
                    _musicVolume = newValue;
                    break;
                case AudioVolumeSetting.Sfx:
                    paramRef = _sfxVolumeParam;
                    _sfxVolume = newValue;
                    break;
                case AudioVolumeSetting.Ui:
                    paramRef = _uiVolumeParam;
                    _uiVolume = newValue;
                    break;
                case AudioVolumeSetting.Amb:
                    paramRef = _ambVolumeParam;
                    _ambVolume = newValue;
                    break;
                case AudioVolumeSetting.Voice:
                    paramRef = _voiceVolumeParam;
                    _voiceVolume = newValue;
                    break;
            }

            if (paramRef == "") return;

            SetGlobalParameter(paramRef, newValue);
        }

        private void OnValidate()
        {
            if (!MasterBanksAreLoaded) return;
            
            SetVolume(AudioVolumeSetting.Master, _masterVolume);
            SetVolume(AudioVolumeSetting.Music, _musicVolume);
            SetVolume(AudioVolumeSetting.Sfx, _sfxVolume);
            SetVolume(AudioVolumeSetting.Ui, _uiVolume);
            SetVolume(AudioVolumeSetting.Amb, _ambVolume);
            SetVolume(AudioVolumeSetting.Voice, _voiceVolume);
        }

        [Serializable]
        private class PlayerPrefsData
        {
            public string Master;
            public string Music;
            public string Sfx;
            public string Ui;
            public string Amb;
            public string Voice;

            private float GetPlayerPrefFloat(string name)
            {
                if (name == Master)
                    return !PlayerPrefs.HasKey(name) ? _defaultMasterValue : PlayerPrefs.GetFloat(name);
                
                return !PlayerPrefs.HasKey(name) ? _defaultValue : PlayerPrefs.GetFloat(name);
            }

            public float MasterValue => GetPlayerPrefFloat(Master);
            public float MusicValue => GetPlayerPrefFloat(Music);
            public float SfxValue => GetPlayerPrefFloat(Sfx);
            public float UiValue => GetPlayerPrefFloat(Ui);
            public float AmbValue => GetPlayerPrefFloat(Amb);
            public float VoiceValue => GetPlayerPrefFloat(Voice);
        }
    }
}