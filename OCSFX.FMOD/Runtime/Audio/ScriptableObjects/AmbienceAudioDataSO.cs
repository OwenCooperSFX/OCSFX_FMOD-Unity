using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Ambience Audio Data")]
    public class AmbienceAudioDataSO : AudioDataSO
    {
        // Fields
        [Header("Events")]
        [SerializeField] private EventReference _ambDefault;

        [SerializeField] private List<EventReference> _levelAmbs = new List<EventReference>(1);

        private readonly Dictionary<string, EventInstance> _instances = new Dictionary<string, EventInstance>();

        public List<EventReference> LevelAmbs => _levelAmbs;

        // Methods
        public void PlayAmbience(int levelNumber, GameObject soundSource)
        {
            if (_levelAmbs.Count < 1) return;
            var eventRef = _levelAmbs[levelNumber];

            StartEvent(eventRef, soundSource);
        }
        
        public void StopAmbience(int levelNumber)
        {
            if (_levelAmbs.Count < 1) return;
            var eventRef = _levelAmbs[levelNumber];

            StopEventGlobal(eventRef);
        }

        private void OnValidate()
        {
            if (_ambDefault.IsNull) return;
            
            if (_levelAmbs[0].IsNull)
            {
                _levelAmbs[0] = _ambDefault;
            }
        }
    }
}