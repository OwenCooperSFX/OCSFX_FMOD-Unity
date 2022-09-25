using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace OCSFX.FMOD
{
    public enum AudioVolumeSetting { Master, Sfx, Music, Ui, Amb, Voice }
    public enum AudioSurface { Stone, Dirt, Grass, Wood, Leaves, Water, Metal }

    public static class AudioStatics
    {
        public static Action MasterBanksLoaded;

        public static Action UiButtonPressed;
        public static Action UiMenuOpened;
        public static Action UiMenuClosed;
        public static Action UiElementFocused;
        public static Action UiGameStarted;
        public static Action UiGameQuit;
        public static Action UiPauseMenuOpened;
        public static Action UiPauseMenuClosed;

        public static bool MasterBanksAreLoaded = false;

        public static EventInstance StartEvent(EventReference eventRef, GameObject sourceObject)
        {
            if (!sourceObject) return new EventInstance();
            
            sourceObject.TryGetComponent<FMODGameObject>(out var audioGameObject);

            if (!audioGameObject) audioGameObject = sourceObject.AddComponent<FMODGameObject>();
                
            return audioGameObject.PlayEvent(eventRef);
        }
        
        public static EventInstance StartEvent(EventReference eventRef, GameObject sourceObject, string parameter = default, float value = default)
        {
            if (!sourceObject) return new EventInstance();
            
            sourceObject.TryGetComponent<FMODGameObject>(out var audioGameObject);

            if (!audioGameObject) audioGameObject = sourceObject.AddComponent<FMODGameObject>();
                
            return audioGameObject.PlayEvent(eventRef, parameter, value);
        }

        public static void StopEvent(EventReference eventRef, GameObject sourceObject)
        {
            if (!sourceObject) return;
            
            sourceObject.TryGetComponent<FMODGameObject>(out var audioGameObject);

            if (!audioGameObject) audioGameObject = sourceObject.AddComponent<FMODGameObject>();
                
            audioGameObject.StopEvent(eventRef);
        }

        public static void StopEventGlobal(EventReference eventRef)
        {
            var eventDesc = RuntimeManager.GetEventDescription(eventRef);
            
            eventDesc.getInstanceList(out var instanceList);

            if (instanceList.Length < 1) return;

            instanceList[0].release();
            instanceList[0].stop(STOP_MODE.ALLOWFADEOUT);
        }
        
        public static EventInstance PlaySound2d(EventReference eventRef)
        {
            var newInstance = RuntimeManager.CreateInstance(eventRef);

            newInstance.start();
            newInstance.release();

            return newInstance;
        }

        public static void StopSoundInstance(EventInstance instance)
        {
            instance.stop(STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }

        public static void PlaySoundOneShot(EventReference eventRef, Vector3 position = default) =>
            RuntimeManager.PlayOneShot(eventRef, position);

        public static void PlaySoundOneShotAttached(EventReference eventRef, GameObject soundSource) =>
            RuntimeManager.PlayOneShotAttached(eventRef, soundSource);

        public static void SetSoundParameter(EventInstance soundEventInstance, string parameterName, float value)
        {
            if (!soundEventInstance.isValid()) return;
            
            // soundEventInstance.getPlaybackState(out var playbackState);
            //
            // if (playbackState == PLAYBACK_STATE.PLAYING) return;

            soundEventInstance.setParameterByName(parameterName, value);
        }

        public static void SetSoundParameter(EventInstance soundEventInstance, PARAMETER_ID parameterID, float value)
        {
            if (!soundEventInstance.isValid()) return;
            soundEventInstance.setParameterByID(parameterID, value);
        }

        public static void SetSoundParameter(GameObject sourceObject, string parameterName, float value)
        {
            //Debug.Log("Set Sound parameter");
            if (!sourceObject) return;
            
            sourceObject.TryGetComponent<FMODGameObject>(out var audioGameObject);

            if (!audioGameObject) audioGameObject = sourceObject.AddComponent<FMODGameObject>();
            
            audioGameObject.SetParameter(parameterName,value);
        }
        
        public static void SetGlobalParameter(string paramRef, float newValue)
        {
            RuntimeManager.StudioSystem.setParameterByName(paramRef, newValue);
        }

        public static void TriggerParameter(string paramRef)
        {
            // Sets Parameter value to 1. Command instrument in FMOD should reset it back to 0.
            // This is to try to accomodate a trigger action like in Wwise.
            RuntimeManager.StudioSystem.setParameterByName(paramRef, 1);
        }
    }
}

