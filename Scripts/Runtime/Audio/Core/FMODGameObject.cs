using System;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace OCSFX.FMOD
{
    public class FMODGameObject : MonoBehaviour
    {
        private List<EventInstance> _attachedInstances = new List<EventInstance>();

        private readonly Dictionary<string, float> _parameters = new Dictionary<string, float>();

        public List<EventInstance> AttachedInstances => _attachedInstances;

        public EventInstance PlayEvent(EventReference eventRef, string parameter = default, float value = default)
        {
            if (eventRef.IsNull) return new EventInstance();
            
            var eventDesc = RuntimeManager.GetEventDescription(eventRef);
            
            eventDesc.createInstance(out var newInstance);

            RuntimeManager.AttachInstanceToGameObject(newInstance, gameObject.transform);
            _attachedInstances.Add(newInstance);

            newInstance.start();
            if (parameter != default)
            {
                newInstance.setParameterByName(parameter, value);
            }
            newInstance.release();

            //Debug.Log(_attachedInstances.Count);
            return newInstance;
        }

        public void StopEvent(EventReference eventRef, bool allowFadeout = true)
        {
            if (eventRef.IsNull) return;
            
            var eventDesc = RuntimeManager.GetEventDescription(eventRef);
            
            eventDesc.getInstanceList(out var instanceList);

            if (instanceList.Length < 1) return; 
            
            var invalidInstances = new List<EventInstance>();
            
            var instance = new EventInstance();
            foreach (var eventInstance in instanceList)
            {
                if (!_attachedInstances.Contains(eventInstance)) return;

                if (!eventInstance.isValid()) continue;
                
                eventInstance.getPlaybackState(out var playbackState);

                if (playbackState is PLAYBACK_STATE.STOPPED or PLAYBACK_STATE.STOPPING)
                {
                    continue;
                }

                instance = eventInstance;
                break;
            }

            foreach (var eventInstance in _attachedInstances)
            {
                if (!eventInstance.isValid())
                {
                    invalidInstances.Add(eventInstance);
                }
            }

            foreach (var eventInstance in invalidInstances)
            {
                if (_attachedInstances.Contains(eventInstance))
                {
                    _attachedInstances.Remove(eventInstance);
                }
            }
            _attachedInstances.TrimExcess();
            
            instance.Stop(allowFadeout);
        }

        public void SetParameter(EventReference eventRef, string parameterName, float value)
        {
            var eventDesc = RuntimeManager.GetEventDescription(eventRef);
            
            eventDesc.getParameterDescriptionByName(parameterName, out var paramDesc);
            eventDesc.getInstanceList(out var instanceList);

            var parameterID = paramDesc.id;

            var activeInstances = new List<EventInstance>();
            foreach (var eventInstance in instanceList)
            {
                if (_attachedInstances.Contains(eventInstance))
                {
                    activeInstances.Add(eventInstance);
                    break;
                }
            }

            foreach (var eventInstance in activeInstances)
            {
                eventInstance.setParameterByID(parameterID, value);
            }
        }

        public void SetParameter(string parameterName, float value)
        {
            //Debug.Log("Set Parameter " + parameterName + " to " + value);
            
            if (!_parameters.TryAdd(parameterName, value))
            {
                _parameters[parameterName] = value;
            }
            
            ApplyParameters();
        }

        private void ApplyParameters()
        {
            //Debug.Log("Apply Parameters");
            
            foreach (var eventInstance in _attachedInstances)
            {
                eventInstance.getPlaybackState(out var playbackState);
                switch (playbackState)
                {
                    case PLAYBACK_STATE.PLAYING:
                    case PLAYBACK_STATE.STARTING:
                    case PLAYBACK_STATE.SUSTAINING:
                    break;
                    case PLAYBACK_STATE.STOPPING:
                    case PLAYBACK_STATE.STOPPED:
                    default:
                    continue;
                }
                
                foreach (var (paramKey, paramValue) in _parameters)
                {
                    eventInstance.setParameterByName(paramKey, paramValue);
                    //Debug.Log("Set " + paramKey + " to " + paramValue);
                }
            }
        }

        private void LateUpdate()
        {
            // Consider making this an InvokeRepeating on a less-frequent tick.
            CleanUpDeadInstances();
        }

        private void CleanUpDeadInstances()
        {
            if (_attachedInstances.Count < 1) return;

            var toRemoveList = new List<EventInstance>();
            
            foreach (var instance in _attachedInstances)
            {
                if (instance.isValid()) continue;
                
                toRemoveList.Add(instance);
            }

            foreach (var instance in toRemoveList)
            {
                instance.release();
                _attachedInstances.Remove(instance);
            }
            
            _attachedInstances.TrimExcess();
        }
    }
}