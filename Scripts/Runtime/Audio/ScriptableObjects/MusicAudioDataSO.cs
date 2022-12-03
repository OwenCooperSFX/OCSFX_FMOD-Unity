using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Music Audio Data")]
    public class MusicAudioDataSO : AudioDataSO
    {
        // Fields
        [Header("Music Events")]
        [SerializeField] private EventReference _mainMenu;
        [SerializeField] private EventReference _gameplayMain;
        [SerializeField] private EventReference _gameplayAlert;
        [SerializeField] private EventReference _endScreen;
        
        [Header("Parameters")]
        [SerializeField] [ParamRef] private string _horizontalParameter;
        [SerializeField] MusicHorizontalParam _horizontalParamValue;
        [Space(8)]
        [SerializeField] [ParamRef] private string _verticalParameter;
        [SerializeField] [Range(1,3)] private int _verticalParamValue;
        // Add more as needed for project.

        private Dictionary<string, EventInstance> _instances = new Dictionary<string, EventInstance>();
        private EventInstance _currentInstance;

        
        // Properties
        public EventReference MainMenu => _mainMenu;
        public EventReference GameplayMain => _gameplayMain;
        public EventReference GameplayAlert => _gameplayAlert;
        public EventReference EndScreen => _endScreen;

        
        // Methods

        protected override void OnMasterBanksLoaded()
        {
            OnValidate();
            SetVerticalParameter(_verticalParamValue);
            SetHorizontalParameter(_horizontalParamValue);
        }
        
        public void PlayMainMenuMusic() => PlayMusic(_mainMenu);
        public void PlayGameplayMainMusic() => PlayMusic(_gameplayMain);
        public void PlayGameplayAlertMusic() => PlayMusic(_gameplayAlert);
        public void PlayEndScreenMusic() => PlayMusic(_endScreen);

        public void StopAllMusic()
        {
            foreach (var entry in _instances)
            {
                entry.Value.Stop();
            }

            _instances.Clear();
        }
        
        private void PlayMusic(EventReference eventRef)
        {
            var eventRefID = eventRef.ToString();

            if (_instances.ContainsKey(eventRefID)) return;

            if (_currentInstance.isValid()) _currentInstance.Stop();
            
            var newInstance = eventRef.Play2D();
            _currentInstance = newInstance;
            
            Debug.Log("Music: " + eventRef);
            
            _instances.Add(eventRefID, eventRef.Play2D());
        }

        private void StopMusic(EventReference eventRef)
        {
            var eventRefID = eventRef.ToString();

            if (!_instances.ContainsKey(eventRefID)) return;

            _instances[eventRefID].Stop();
            _instances.Remove(eventRefID);
        }

        // Enable for event-level parameters rather than global.
        /*
        public void SetHorizonalParameter(EventReference eventRef, MusicHorizontalParam value)
        {
            if (!_instances.TryGetValue(eventRef.ToString(), out var instance))
            {
                Debug.LogError($"{eventRef.GetEventName()} not found in {this} instances.");
                return;
            }
            
            Debug.Log($"Set {eventRef.GetEventName()} parameter {_horizontalParameter} value to {value}.");
            instance.setParameterByName(_horizontalParameter, (int)value);
        }
        
        public void SetVerticalParameter(EventReference eventRef, int value)
        {
            if (!_instances.TryGetValue(eventRef.ToString(), out var instance))
            {
                Debug.LogError($"{eventRef.GetEventName()} not found in {this} instances.");
                return;
            }
            
            Debug.Log($"Set {eventRef.GetEventName()} parameter {_verticalParameter} value to {value}.");
            instance.setParameterByName(_verticalParameter, value);
        }
        */

        public void SetVerticalParameter(int verticalParamValue)
        {
            _verticalParamValue = verticalParamValue;
            AudioStatics.SetFMODParameterGlobal(_verticalParameter, _verticalParamValue);
        }

        public void SetHorizontalParameter(MusicHorizontalParam horizontalParamValue)
        {
            _horizontalParamValue = horizontalParamValue;
            AudioStatics.SetFMODParameterGlobal(_horizontalParameter, (int)_horizontalParamValue);
        }
        
        public void SetHorizontalParameter(int horizontalParamValue)
        {
            _horizontalParamValue = (MusicHorizontalParam)horizontalParamValue;
            AudioStatics.SetFMODParameterGlobal(_horizontalParameter, (int)_horizontalParamValue);
        }

        public void ResetParameters()
        {
            SetHorizontalParameter(MusicHorizontalParam.Int1);
            SetVerticalParameter(1);
        }

        private void OnValidate()
        {
            if (!Application.isPlaying) return;
            
            SetVerticalParameter(_verticalParamValue);
            SetHorizontalParameter(_horizontalParamValue);
        }
    }
}