using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    public class AudioManager : Singleton<AudioManager>
    {
        // Fields
        [Header("Settings")]
        [SerializeField] private VolumeSettingsAudioDataSO _volumeSettings;

        [Header("Audio Data")]
        [SerializeField] private SoundbanksAudioDataSO _soundbanksAudioData;
        [SerializeField] private UiAudioDataSO _uiAudioData;
        [SerializeField] private MusicAudioDataSO _musicAudioData;
        [SerializeField] private AmbienceAudioDataSO _ambienceAudioData;
        [SerializeField] private SnapshotsAudioDataSO _snapshotsAudioData;
        [SerializeField] private DialogAudioDataSO _dialogAudioData;

        [Header("Testing/Debugging")]
        [SerializeField] private bool _playTestEventOnStart;
        [SerializeField] private EventReference _testEventRef;
        [SerializeField] private GameObject _testGameObject;
        
        private GameObject _listenerObject;

        protected override void Awake()
        {
            base.Awake();
            ValidateListener();
        }

        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        public void StartTestEvent() => StartEvent(_testEventRef, _testGameObject);
        public void StopTestEvent() => StopEvent(_testEventRef, _testGameObject);

        // private void LoadStartupSoundbanksAsync() =>
        //      StartCoroutine(Co_LoadStartupSoundbanks());

        private void OnMasterBanksLoaded()
        {
            // Now that the MasterBanks have been loaded, we can start playing sounds (e.g. music, ambience, UI)
            
            if (_playTestEventOnStart)
                StartTestEvent();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            // Load scene-specific banks, start scene-specific audio, etc.
        }
        
        private void OnSceneUnloaded(Scene scene)
        {
            // Unload scene-specific banks, stop audio, etc.
        }

        // Helpers
        private void SubscribeEvents()
        {
            MasterBanksLoaded += OnMasterBanksLoaded;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void UnsubscribeEvents()
        {
            MasterBanksLoaded -= OnMasterBanksLoaded;

            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnValidate()
        {
            if (!_testGameObject) _testGameObject = gameObject;
        }

        private void ValidateListener()
        {
            if (!_listenerObject) _listenerObject = Camera.main.gameObject;
            if (!_listenerObject) _listenerObject = Instance.gameObject;

            if (!_listenerObject.TryGetComponent<AudioListener>(out var unityAudioListener)) return;
            
            Destroy(unityAudioListener);
            _listenerObject.AddComponent<StudioListener>();
        }
    }
}