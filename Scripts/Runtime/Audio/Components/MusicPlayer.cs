using OCSFX.FMOD.AudioData;
using UnityEngine;
using UnityEngine.SceneManagement;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicAudioDataSO _musicAudioData;

        [Header("Settings")]
        [SerializeField] private bool _resetParametersOnStart = true;
    
        private int _horizontalParamValue = 0;
    
        private void OnEnable()
        {
            MasterBanksLoaded += OnMasterBanksLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            if (_resetParametersOnStart)
                _musicAudioData.ResetParameters();
        }
    
        private void OnDisable()
        {
            MasterBanksLoaded -= OnMasterBanksLoaded;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _horizontalParamValue = scene.name switch
            {
                "MainMenu" => 1,
                "EndScreen" => 1,
                "Game" => 0,
                "Level2" => 2,
                _ => 0
            };
            
            if (MasterBanksAreLoaded)
                _musicAudioData.SetHorizontalParameter(_horizontalParamValue);
        }
    
        private void OnMasterBanksLoaded()
        {
            _musicAudioData.ResetParameters();
            _musicAudioData.PlayGameplayMainMusic();
            
            _musicAudioData.SetHorizontalParameter(_horizontalParamValue);
        }
    
        public void IncrementVerticalParameter()
        {
            
        }
    
        public void DecrementVerticalParameter()
        {
            
        }
    }

}
