using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using static OCSFX.FMOD.AudioStatics;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Music Audio Data")]
    public class MusicAudioDataSO : AudioDataSO
    {
        // Fields
        [Header("Music Events")]
        [SerializeField] private EventReference _mainMenu;
        [SerializeField] private EventReference _gameplay;
        [SerializeField] private EventReference _endScreen;
        
        [Header("Parameters")]
        [SerializeField] [ParamRef] private string _musicParameter;
        // Add more as needed for project.

        private Dictionary<string, EventInstance> _instances = new Dictionary<string, EventInstance>();
        private EventInstance _currentInstance;

        // Properties
        public EventReference MainMenu => _mainMenu;
        public EventReference Gameplay => _gameplay;
        public EventReference EndScreen => _endScreen;

        // Methods

        public void PlayMainMenuMusic() => PlayMusic(_mainMenu);
        public void PlayGameplayMusic() => PlayMusic(_gameplay);
        public void PlayEndScreenMusic() => PlayMusic(_endScreen);

        public void StopAllMusic()
        {
            foreach (var entry in _instances)
            {
                StopSoundInstance(entry.Value);
            }

            _instances.Clear();
        }
        
        private void PlayMusic(EventReference eventRef)
        {
            var eventRefID = eventRef.ToString();

            if (_instances.ContainsKey(eventRefID)) return;

            StopSoundInstance(_currentInstance);
            var newInstance = PlaySound2d(eventRef);
            _currentInstance = newInstance;
            
            Debug.Log("Music: " + eventRef);
            
            _instances.Add(eventRefID, PlaySound2d(eventRef));
        }

        private void StopMusic(EventReference eventRef)
        {
            var eventRefID = eventRef.ToString();

            if (!_instances.ContainsKey(eventRefID)) return;
            
            _instances[eventRefID].stop(STOP_MODE.ALLOWFADEOUT);
            _instances[eventRefID].release();
            _instances.Remove(eventRefID);
        }

        public void SetMusicParameter(EventReference eventRef, float value)
        {
            if (!_instances.TryGetValue(eventRef.ToString(), out var instance)) return;
            
            SetSoundParameter(instance, _musicParameter, value);
        }
    }
}