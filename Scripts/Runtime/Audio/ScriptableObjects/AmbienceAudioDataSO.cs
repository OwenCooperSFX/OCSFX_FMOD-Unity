using System;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Ambience")]
    public class AmbienceAudioDataSO : AudioDataSO
    {
        // Fields
        [Header("Events")]
        [SerializeField] private EventReference _ambDefault;

        [SerializeField] private List<EventReference> _levelAmbs = new List<EventReference>(1);
        
        private List<EventReference> _eventHistory = new List<EventReference>();

        // Properties
        public List<EventReference> LevelAmbs => _levelAmbs;

        
        // Methods
        protected override void OnEnable()
        {
            base.OnDisable();
            _eventHistory.Clear();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _eventHistory.Clear();
        }

        protected override void OnMasterBanksLoaded()
        {
            //PlayDefaultAmbience();
        }

        public void PlayDefaultAmbience()
        {
            _ambDefault.Play2D();
            _eventHistory.Add(_ambDefault);
            
            if (!_eventHistory.Contains(_ambDefault))
                _eventHistory.Add(_ambDefault);

            var previous = GetIndex(_ambDefault) - 1;
            
            if (previous >= 0)
                _eventHistory[previous].StopGlobal();
        }

        public void StopDefaultAmbience()
        {
            _ambDefault.StopGlobal();

            var previous = GetIndex(_ambDefault) - 1;

            if (previous >= 0 && !_eventHistory[previous].IsNull)
            {
                _eventHistory[previous].Play2D();
            }
            
            if (_eventHistory.Contains(_ambDefault))
                _eventHistory.Remove(_ambDefault);
        }

        public void PlayAmbience(int levelIndex, GameObject soundSource = null)
        {
            if (_levelAmbs.Count < 1) return;
            var eventRef = _levelAmbs[levelIndex];

            eventRef.Play(soundSource);
            
            if (!_eventHistory.Contains(eventRef))
                _eventHistory.Add(eventRef);

            var previous = GetIndex(eventRef) - 1;
            
            if (previous >= 0)
                _eventHistory[previous].StopGlobal();
        }
        
        public void PlayAmbience(int levelIndex)
        {
            if (_levelAmbs.Count < 1) return;
            var eventRef = _levelAmbs[levelIndex];

            eventRef.Play2D();
            
            if (!_eventHistory.Contains(eventRef))
                _eventHistory.Add(eventRef);

            var previous = GetIndex(eventRef) - 1;
            
            if (previous >= 0)
                _eventHistory[previous].StopGlobal();
        }

        private int GetIndex(EventReference eventRef)
        {
            var predicate = new Predicate<EventReference>(isMatch);
            bool isMatch(EventReference eventRefToCheck)
            {
                return eventRefToCheck.GetEventName() == eventRef.GetEventName();
            }

            var existing = _eventHistory.FindIndex(isMatch);

            return existing;
        }
        
        public void StopAmbience(int index)
        {
            if (_levelAmbs.Count < 1) return;
            var eventRef = _levelAmbs[index];

            eventRef.StopGlobal();

            var previous = GetIndex(eventRef) - 1;

            if (previous >= 0 && !_eventHistory[previous].IsNull)
            {
                _eventHistory[previous].Play2D();
            }

            if (_eventHistory.Contains(_ambDefault))
                _eventHistory.Remove(eventRef);
        }

        private void OnValidate()
        {
            if (_ambDefault.IsNull) return;
            if (_levelAmbs.Count < 1) return;

            if (_levelAmbs[0].IsNull)
            {
                _levelAmbs[0] = _ambDefault;
            }
        }
    }
}