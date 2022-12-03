using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace OCSFX.FMOD
{
    public class FMODInstanceLimiter: MonoBehaviour
    {
        [Serializable]
        private enum LimitBehavior
        {
            KillOldest,
            PreventNew
        };

        [SerializeField] private LimitBehavior _instanceLimitBehavior;
            
        [SerializeField]
        private List<Generics.KeyValuePair<EventReference, int>> eventInstanceLimits
            = new List<Generics.KeyValuePair<EventReference, int>>();

        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        private FMODGameObject _fmodGameObject;
        
        private Dictionary<string, List<EventInstance>> _activeInstances
            = new Dictionary<string, List<EventInstance>>();

        private void Awake()
        {
            if (!TryGetComponent(out _fmodGameObject))
            {
                _fmodGameObject = gameObject.AddComponent<FMODGameObject>();
            }

            foreach (var entry in eventInstanceLimits)
            {
                _dictionary.Add(entry.Key.GetEventName(), entry.Value);
            }
        }

        private void Update()
        {
            TryGetComponent(out _fmodGameObject);
            
            if (_fmodGameObject.AttachedInstances.Count < 1) return;

            foreach (var eventInstance in _fmodGameObject.AttachedInstances)
            {
                var eventName = eventInstance.GetEventName();
                if (_dictionary.ContainsKey(eventName))
                {
                    if (!_activeInstances.ContainsKey(eventName))
                    {
                        _activeInstances.Add(eventName, new List<EventInstance>());
                    }

                    _activeInstances[eventName].Add(eventInstance);
                }
            }

            foreach (var fmodEvent in _activeInstances)
            {
                var eventName = fmodEvent.Key;

                if (_dictionary.ContainsKey(eventName))
                {
                    Debug.Log("Contains key");
                    var limit = _dictionary[eventName];
                    var instanceList = fmodEvent.Value;

                    if (instanceList.Count < limit) continue;
                    
                    switch (_instanceLimitBehavior)
                    {
                        case LimitBehavior.KillOldest:
                            instanceList[0].Stop(false);
                            break;
                        case LimitBehavior.PreventNew:
                            while (instanceList.Count > limit)
                            {
                                var endIndex = instanceList.Count - 1;
                                instanceList[endIndex].Stop(false);
                                instanceList.RemoveAt(instanceList.Count - 1);
                            }
                            break;
                    }
                }
            }
        }
    }
}