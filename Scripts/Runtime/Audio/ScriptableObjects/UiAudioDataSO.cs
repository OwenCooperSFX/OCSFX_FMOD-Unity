using UnityEngine;
using FMODUnity;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/UI")]
    public class UiAudioDataSO : AudioDataSO
    {
        // Fields
        [Header("User-Input Events")]
        [SerializeField] private EventReference _playButtonPress;
        [SerializeField] private EventReference _quitButtonPress;
        [SerializeField] private EventReference _backButtonPress;
        [SerializeField] private EventReference _buttonPress;
        [SerializeField] private EventReference _focus;
        [SerializeField] private EventReference _unfocus;
        [SerializeField] private EventReference _menuOpen;
        [SerializeField] private EventReference _menuClose;
        [SerializeField] private EventReference _pauseMenuOpen;
        [SerializeField] private EventReference _pauseMenuClose;
        
        //[Header("Gameplay Events")]
        
        //[Header("Parameters")]

        /* Methods */
        
        // User-Input Events
        public void OnGameplayStart() => _playButtonPress.PlayOneShot();
        public void OnApplicationQuit() => _quitButtonPress.PlayOneShot();
        public void OnUiSelect() => _buttonPress.PlayOneShot();
        public void OnUiBack() => _backButtonPress.PlayOneShot();
        public void OnUiFocus() => _focus.PlayOneShot();
        public void OnUiUnfocus() => _unfocus.PlayOneShot();
        public void OnMenuOpen() => _menuOpen.PlayOneShot();
        public void OnMenuClose() => _menuClose.PlayOneShot();
        public void OnPauseMenuOpen() => _pauseMenuOpen.PlayOneShot();
        public void OnPauseMenuClose() => _pauseMenuClose.PlayOneShot();

        // Gameplay Events

        protected override void OnMasterBanksLoaded()
        {
            // Await this for initialization to ensure that banks are loaded
        }
    }
}