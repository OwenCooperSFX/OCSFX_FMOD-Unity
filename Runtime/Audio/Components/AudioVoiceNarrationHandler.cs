using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static OCSFX.FMOD.AudioStatics;
using Debug = UnityEngine.Debug;

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
            _currentNarrationInstance = StartEvent(_currentNarrationEvent, gameObject);
        }
        
        public void KillNarration()
        {
            _snapshotsAudioData.SetDialogSnapshot(false);
            StopSoundInstance(_currentNarrationInstance);
        }
    }
}