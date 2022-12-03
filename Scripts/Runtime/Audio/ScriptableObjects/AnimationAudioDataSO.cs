using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Animation")]
    public class AnimationAudioDataSO : AudioDataSO
    {
        [SerializeField]
        private List<OCSFX.Generics.KeyValuePair<string, EventReference>> _events
            = new List<OCSFX.Generics.KeyValuePair<string, EventReference>>();
     
        private readonly Dictionary<string, EventReference> _dictionary = new Dictionary<string, EventReference>();
     
        protected override void OnEnable()
        {
            base.OnEnable();
            RebuildDictionary();
        }

        public bool TryGetAnimSoundEvent(string animEventName, out EventReference soundEvent)
        {
            return _dictionary.TryGetValue(animEventName, out soundEvent);
        }

        public EventReference GetAnimSoundEvent(string animEventName)
        {
            return _dictionary[animEventName];
        }

        private void RebuildDictionary()
        {
            _dictionary.Clear();
            
            foreach (var entry in _events)
            {
                _dictionary.TryAdd(entry.Key, entry.Value);
                //Debug.Log(entry.Key + ": " + entry.Value.GetEventName());
            }
            
            _dictionary.TrimExcess();
        }
     
        private void OnValidate()
        {
            RebuildDictionary();
        }
    }
}