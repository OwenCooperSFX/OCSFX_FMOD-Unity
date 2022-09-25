using UnityEngine;
using FMODUnity;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/UI Audio Data")]
    public class UiAudioDataSO : AudioDataSO
    {
        // Fields
        [Header("User-Input Events")]
        [SerializeField] private EventReference _playButtonPress;
        [SerializeField] private EventReference _quitButtonPress;
        [SerializeField] private EventReference _buttonPress;
        [SerializeField] private EventReference _hover;
        [SerializeField] private EventReference _menuOpen;
        [SerializeField] private EventReference _menuClose;
        [SerializeField] private EventReference _pauseMenuOpen;
        [SerializeField] private EventReference _pauseMenuClose;

        // Properties
        public EventReference PlayButtonPress => _playButtonPress;
        public EventReference QuitButtonPress => _quitButtonPress;
        public EventReference ButtonPress => _buttonPress;
        public EventReference Hover => _hover;
        public EventReference MenuOpen => _menuOpen;
        public EventReference MenuClose => _menuClose;
        public EventReference PauseMenuOpen => _pauseMenuOpen;
        public EventReference PauseMenuClose => _pauseMenuClose;

        // Methods
        public void PlayUiGameStart() => PlaySoundOneShot(_playButtonPress);
        public void PlayUiGameQuit() => PlaySoundOneShot(_quitButtonPress);
        public void PlayUiSelect() => PlaySoundOneShot(_buttonPress);
        public void PlayUiHover() => PlaySoundOneShot(_hover);
        public void PlayUiMenuOpen() => PlaySoundOneShot(_menuOpen);
        public void PlayUiMenuClose() => PlaySoundOneShot(_menuClose);
        public void PlayUiPauseMenuOpen() => PlaySoundOneShot(_pauseMenuOpen);
        public void PlayUiPauseMenuClose() => PlaySoundOneShot(_pauseMenuClose);

        private void OnEnable()
        {
            UiButtonPressed += PlayUiSelect;
            UiElementFocused += PlayUiHover;
            UiMenuOpened += PlayUiMenuOpen;
            UiMenuClosed += PlayUiMenuClose;
            UiGameStarted += PlayUiGameStart;
            UiGameQuit += PlayUiGameQuit;
            UiPauseMenuOpened += PlayUiPauseMenuOpen;
            UiPauseMenuClosed += PlayUiPauseMenuClose;
        }

        private void OnDisable()
        {
            UiButtonPressed -= PlayUiSelect;
            UiElementFocused -= PlayUiHover;
            UiMenuOpened -= PlayUiMenuOpen;
            UiMenuClosed -= PlayUiMenuClose;
            UiGameStarted -= PlayUiGameStart;
            UiGameQuit -= PlayUiGameQuit;
            UiPauseMenuOpened -= PlayUiPauseMenuOpen;
            UiPauseMenuClosed -= PlayUiPauseMenuClose;
        }
    }
}