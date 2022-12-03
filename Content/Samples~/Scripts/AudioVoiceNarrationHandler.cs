using FMOD.Studio;
using FMODUnity;
using OCSFX.FMOD.AudioData;
using UnityEngine;

namespace OCSFX.FMOD
{
    public class AudioVoiceNarrationHandler: MonoBehaviour
    {
        [SerializeField] private DialogAudioDataSO _dialogAudioData;
        [SerializeField] private SnapshotsAudioDataSO _snapshotsAudioData;

        private EventInstance _currentNarrationInstance;
        private EventReference _currentNarrationEvent;

        public void BeginNarration(EventReference narratorVoiceEvent)
        {
            _snapshotsAudioData.SetDialogSnapshot(true);
            _currentNarrationEvent = narratorVoiceEvent;
            _currentNarrationInstance = _currentNarrationEvent.Play(gameObject);
        }
        
        public void KillNarration()
        {
            _snapshotsAudioData.SetDialogSnapshot(false);
            _currentNarrationInstance.Stop();
        }
    }
}