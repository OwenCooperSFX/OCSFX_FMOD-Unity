using System;
using UnityEngine;
using FMODUnity;
using OCSFX.UI;
using UnityEngine.UI;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD.AudioData
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

        [Space(8)]
        private PlayerPrefsAudioData _playerPrefsAudioData;

        private const float _defaultValue = 1.0f;
        private const float _defaultMasterValue = 0.9f;

        protected override void OnMasterBanksLoaded() => LoadPlayerPrefs();

        private void LoadPlayerPrefs()
        {
            SetVolume(AudioVolumeSetting.Master, _playerPrefsAudioData.MasterValue);
            SetVolume(AudioVolumeSetting.Music, _playerPrefsAudioData.MusicValue);
            SetVolume(AudioVolumeSetting.Sfx, _playerPrefsAudioData.SfxValue);
            SetVolume(AudioVolumeSetting.Ui, _playerPrefsAudioData.UiValue);
            SetVolume(AudioVolumeSetting.Amb, _playerPrefsAudioData.AmbValue);
            SetVolume(AudioVolumeSetting.Voice, _playerPrefsAudioData.VoiceValue);
        }

        // Enable to use AudioSliderPlayerPrefHandler
        /*
        public void SetMasterVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Master, slider.value);
        
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);
        
            _playerPrefsAudioData.Master = sliderPlayerPrefHandler.PlayerPrefName;
        }
        
        public void SetMusicVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Music, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);
        
            _playerPrefsAudioData.Music = sliderPlayerPrefHandler.PlayerPrefName;
            
        }
        
        public void SetSfxVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Sfx, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);
        
            _playerPrefsAudioData.Sfx = sliderPlayerPrefHandler.PlayerPrefName;
        }
        
        public void SetUiVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Ui, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);
        
            _playerPrefsAudioData.Ui = sliderPlayerPrefHandler.PlayerPrefName;
        }
        
        public void SetAmbVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Amb, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);
        
            _playerPrefsAudioData.Amb = sliderPlayerPrefHandler.PlayerPrefName;
        }
        
        public void SetVoiceVolume(Slider slider)
        {
            SetVolume(AudioVolumeSetting.Voice, slider.value);
            
            slider.TryGetComponent<AudioSliderPlayerPrefHandler>(out var sliderPlayerPrefHandler);
        
            _playerPrefsAudioData.Voice = sliderPlayerPrefHandler.PlayerPrefName;
        }
        */

        private void SetVolume(AudioVolumeSetting setting, float newValue)
        {
            string paramRef = null;

            newValue = Mathf.Clamp(newValue, 0, 1);

            switch (setting)
            {
                case AudioVolumeSetting.Master:
                    paramRef = _masterVolumeParam;
                    _masterVolume = newValue;
                    _playerPrefsAudioData.MasterValue = newValue;
                    break;
                case AudioVolumeSetting.Music:
                    paramRef = _musicVolumeParam;
                    _musicVolume = newValue;
                    _playerPrefsAudioData.MusicValue = newValue;
                    break;
                case AudioVolumeSetting.Sfx:
                    paramRef = _sfxVolumeParam;
                    _sfxVolume = newValue;
                    _playerPrefsAudioData.SfxValue = newValue;
                    break;
                case AudioVolumeSetting.Ui:
                    paramRef = _uiVolumeParam;
                    _uiVolume = newValue;
                    _playerPrefsAudioData.UiValue = newValue;
                    break;
                case AudioVolumeSetting.Amb:
                    paramRef = _ambVolumeParam;
                    _ambVolume = newValue;
                    _playerPrefsAudioData.AmbValue = newValue;
                    break;
                case AudioVolumeSetting.Voice:
                    paramRef = _voiceVolumeParam;
                    _voiceVolume = newValue;
                    _playerPrefsAudioData.VoiceValue = newValue;
                    break;
            }

            if (paramRef == "") return;

            SetFMODParameterGlobal(paramRef, newValue);
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
        public class PlayerPrefsAudioData
        {
            [SerializeField] private string _master = "Audio_Master";
            [SerializeField] private string _music = "Audio_Music";
            [SerializeField] private string _sfx = "Audio_SFX";
            [SerializeField] private string _ui = "Audio_UI";
            [SerializeField] private string _amb = "Audio_Amb";
            [SerializeField] private string _voice = "Audio_Voice";

            public string Master { get => _master; set => _master = value; }
            public string Music { get => _music; set => _music = value; }
            public string Sfx { get => _sfx; set => _sfx = value; }
            public string Ui { get => _ui; set => _ui = value; }
            public string Amb { get => _amb; set => _amb = value; }
            public string Voice { get => _voice; set => _voice = value; }

            private float GetPlayerPrefFloat(string playerPrefName)
            {
                if (playerPrefName == Master)
                    return !PlayerPrefs.HasKey(playerPrefName) ? _defaultMasterValue : PlayerPrefs.GetFloat(playerPrefName);
                
                return !PlayerPrefs.HasKey(playerPrefName) ? _defaultValue : PlayerPrefs.GetFloat(playerPrefName);
            }
            
            private void SetPlayerPrefFloat(string playerPrefName, float value)
            {
                PlayerPrefs.SetFloat(playerPrefName, value);
            }

            public float MasterValue
            {
                get => GetPlayerPrefFloat(Master);
                set => SetPlayerPrefFloat(Master, value);
            }

            public float MusicValue
            {
                get => GetPlayerPrefFloat(Music);
                set => SetPlayerPrefFloat(Music, value);
            }
            public float SfxValue
            {
                get => GetPlayerPrefFloat(Sfx);
                set => SetPlayerPrefFloat(Sfx, value);
            }

            public float UiValue
            {
                get => GetPlayerPrefFloat(Ui);
                set => SetPlayerPrefFloat(Ui, value);
            }

            public float AmbValue
            {
                get => GetPlayerPrefFloat(Amb);
                set => SetPlayerPrefFloat(Amb, value);
            }

            public float VoiceValue
            {
                get => GetPlayerPrefFloat(Voice);
                set => SetPlayerPrefFloat(Voice, value);
            }
        }
    }
}