using System.Collections;
using FMOD.Studio;
using FMODUnity;
using OCSFX.FMOD.AudioData;
using UnityEngine;
using UnityEngine.SceneManagement;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    public class AudioManager : Singleton<AudioManager>
    {
        // Fields
        [Header("Settings")] [SerializeField] private VolumeSettingsAudioDataSO _volumeSettings;

        [Header("Audio Data")] [SerializeField]
        private SoundbanksAudioDataSO _soundbanksAudioData;

        [SerializeField] private UiAudioDataSO _uiAudioData;
        [SerializeField] private MusicAudioDataSO _musicAudioData;
        [SerializeField] private AmbienceAudioDataSO _ambienceAudioData;
        [SerializeField] private SnapshotsAudioDataSO _snapshotsAudioData;
        [SerializeField] private DialogAudioDataSO _dialogAudioData;

        [Header("Testing/Debugging")]
        [SerializeField] private bool _playTestEventOnStart;
        [SerializeField] private bool _playTestMusicOnStart;

        [SerializeField] private EventReference _testEventRef;
        [SerializeField] private GameObject _testGameObject;
        [Space(8)]
        [SerializeField] private EventReference _testMusicRef;

        private EventInstance _testEventInstance;
        private EventInstance _testMusicInstance;

        private GameObject _listenerObject;

        // [SerializeField] private FMODVoiceLine _fmodVoiceLine;

        protected override void Awake()
        {
            base.Awake();
            ValidateListener();
            _soundbanksAudioData.LoadStartupSoundbanks();
        }

        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();
        private void OnValidate() => _testGameObject = _testGameObject ? _testGameObject : gameObject;

        public static void StartTestEvent() => Instance._testEventInstance =  Instance._testEventRef.Play(Instance._testGameObject);

        public static void StopTestEvent()
        {
            var valid = Instance._testEventInstance.isValid();
            
            if (valid) Instance._testEventInstance.Stop();
        }
        
        public static void StartTestMusic() => Instance._testMusicInstance =  Instance._testMusicRef.Play2D();

        public static void StopTestMusic()
        {
            var valid = Instance._testMusicInstance.isValid();
            
            if (valid) Instance._testMusicInstance.Stop();
        }

        private void Start()
        {
            if (!MasterBanksAreLoaded) return;
            
            if (_playTestEventOnStart)
                StartTestEvent();
            
            if (_playTestMusicOnStart)
                StartTestMusic();
        }

        private void OnMasterBanksLoaded()
        {
            // Now that the MasterBanks have been loaded, we can start playing sounds (e.g. music, ambience, UI)

            if (_playTestEventOnStart)
                StartTestEvent();
            
            if (_playTestMusicOnStart)
                StartTestMusic();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            _snapshotsAudioData.ClearAllSnapshots();
            
            var levelIndex = scene.name switch
            {
                "Game" => 0,
                "Level2" => 1,
                _ => 0
            };

            if (!MasterBanksAreLoaded)
                StartCoroutine(Co_AmbienceAwaitBanksLoaded(levelIndex));
            else
                _ambienceAudioData.PlayAmbience(levelIndex);
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

        private void ValidateListener()
        {
            var mainCamera = Camera.main;
            if (!_listenerObject)
                _listenerObject = mainCamera ? mainCamera.gameObject : Instance.gameObject;

            if (!_listenerObject.TryGetComponent<AudioListener>(out var unityAudioListener)) return;

            Destroy(unityAudioListener);
            
            if (!_listenerObject.TryGetComponent<StudioListener>(out var fmodListener))           
                _listenerObject.AddComponent<StudioListener>();
        }

        private void OnDestroy()
        {
            StopTestEvent();
            StopTestMusic();
        }
        
        private IEnumerator Co_AmbienceAwaitBanksLoaded(int levelIndex)
        {
            while (!MasterBanksAreLoaded)
                yield return null;
            
            _ambienceAudioData.PlayAmbience(levelIndex);
        }
    }
}