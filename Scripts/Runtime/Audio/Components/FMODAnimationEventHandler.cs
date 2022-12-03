using OCSFX.FMOD.AudioData;
using UnityEngine;

namespace OCSFX.FMOD
{
     public class FMODAnimationEventHandler : MonoBehaviour
     {
         [SerializeField]
         protected GameObject _soundSource;

         [SerializeField] private AnimationAudioDataSO _audioData;
     
         private void Awake()
         {
             if (!_soundSource) _soundSource = gameObject;
         }
     
         public virtual void StartAudioEvent(string eventName)
         {
             if (!_audioData.TryGetAnimSoundEvent(eventName, out var fmodEventRef))
             {
                 Debug.LogError("'" + eventName + "'" + " was not found in " + this + "'s " + nameof(AnimationAudioDataSO) + " events.");
                 return;
             }
     
             fmodEventRef.Play(_soundSource);
         }
         
         public virtual void StartAudioEvent(string eventName, string parameter, float parameterValue)
         {
             if (!_audioData.TryGetAnimSoundEvent(eventName, out var fmodEventRef))
             {
                 Debug.LogError("'" + eventName + "'" + " was not found in " + this + "'s " + nameof(AnimationAudioDataSO) + " events.");
                 return;
             }
     
             fmodEventRef.Play(_soundSource, parameter, parameterValue);
         }
         
         public virtual void StopAudioEvent(string eventName)
         {
             if (!_audioData.TryGetAnimSoundEvent(eventName, out var fmodEventRef))
             {
                 Debug.LogError("'" + eventName + "'" + " was not found in " + this + "'s " + nameof(AnimationAudioDataSO) + " events.");
                 return;
             }
     
             fmodEventRef.Stop(_soundSource);
         }

         private void OnValidate()
         {
             if (!_soundSource) _soundSource = gameObject;
         }
     }
}