using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Snapshots Audio Data")]
    public class SnapshotsAudioDataSO : AudioDataSO
    {
        [SerializeField] private EventReference _dialog;
        [SerializeField] private EventReference _pauseMenu;
        [SerializeField] private EventReference _mainMenu;
        [SerializeField] private EventReference _narration;
        
        public EventReference Dialog => _dialog;
        public EventReference PauseMenu => _pauseMenu;
        public EventReference MainMenu => _mainMenu;
        public EventReference Narration => _narration;

        public void ClearAllSnapshots()
        {
            StopSnapshot(_dialog);
            StopSnapshot(_pauseMenu);
            StopSnapshot(_mainMenu);
            StopSnapshot(_narration);
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

        private void StartSnapshot(EventReference snapshot)
        {
            PlaySound2d(snapshot);
        }

        private void StopSnapshot(EventReference snapshot)
        {
            StopEventGlobal(snapshot);
        }
    }
}
