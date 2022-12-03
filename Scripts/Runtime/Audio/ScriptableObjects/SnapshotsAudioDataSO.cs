using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Snapshots")]
    public class SnapshotsAudioDataSO : AudioDataSO
    {
        [Header("States")]
        [SerializeField] private EventReference _dialog;
        [SerializeField] private EventReference _pauseMenu;
        [SerializeField] private EventReference _mainMenu;
        [SerializeField] private EventReference _narration;
        [SerializeField] private EventReference _silence;
        
        [Header("Reverb Zones")]
        [SerializeField] private EventReference _reverbIntSmall;
        [SerializeField] private EventReference _reverbIntMed;
        [SerializeField] private EventReference _reverbIntLarge;
        [SerializeField] private EventReference _reverbExtLarge;
        
        
        public EventReference Dialog => _dialog;
        public EventReference PauseMenu => _pauseMenu;
        public EventReference MainMenu => _mainMenu;
        public EventReference Narration => _narration;
        public EventReference Silence => _silence;
        
        public EventReference ReverbIntSmall => _reverbIntSmall;
        public EventReference ReverbIntMed => _reverbIntMed;
        public EventReference ReverbIntLarge => _reverbIntLarge;
        public EventReference ReverbExtLarge => _reverbExtLarge;

        public void ClearAllSnapshots()
        {
            StopSnapshot(_dialog);
            StopSnapshot(_pauseMenu);
            StopSnapshot(_mainMenu);
            StopSnapshot(_narration);
            StopSnapshot(_silence);
            
            StopSnapshot(_reverbIntSmall);
            StopSnapshot(_reverbIntMed);
            StopSnapshot(_reverbIntLarge);
            StopSnapshot(_reverbExtLarge);
        }

        public void SetDialogSnapshot(bool state)
        {
            if (state) StartSnapshot(_dialog);
            else StopSnapshot(_dialog);
        }

        public void SetPauseMenuSnapshot(bool state)
        {
            if (state) StartSnapshot(_pauseMenu);
            else StopSnapshot(_pauseMenu);
        }
        
        public void SetMainMenuSnapshot(bool state)
        {
            if (state) StartSnapshot(_mainMenu);
            else StopSnapshot(_mainMenu);
        }
        
        public void SetNarrationSnapshot(bool state)
        {
            if (state) StartSnapshot(_narration);
            else StopSnapshot(_narration);
        }

        public void SetSilenceSnapshot(bool state)
        {
            if (state) StartSnapshot(_silence);
            else StopSnapshot(_silence);
        }
        
        public void SetReverbSnapshot(ReverbType reverbType, bool state)
        {
            EventReference eventRef = new();

            switch (reverbType)
            {
                case ReverbType.IntSmall:
                    eventRef = _reverbIntSmall;
                    break;
                case ReverbType.IntMed:
                    eventRef = _reverbIntMed;
                    break;
                case ReverbType.IntLarge:
                    eventRef = _reverbIntLarge;
                    break;
                case ReverbType.ExtLarge:
                    eventRef = _reverbExtLarge;
                    break;
            }

            if (state) StartSnapshot(eventRef);
            else StopSnapshot(eventRef);
        }

        private void StartSnapshot(EventReference snapshot)
        {
            snapshot.Play2D();
        }

        private void StopSnapshot(EventReference snapshot)
        {
            snapshot.StopGlobal();
        }
    }
}
