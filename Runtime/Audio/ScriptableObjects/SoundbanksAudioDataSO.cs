using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Soundbanks Audio Data")]
    public class SoundbanksAudioDataSO : AudioDataSO
    {
        [Space(5)] [BankRef] [SerializeField] private List<string> _startupSoundbanks;
        [SerializeField] [Tooltip("A minimum added delay for safety.")] [Range(0f, 1f)]
        private float _minimumLoadTime = 0.1f;

        public List<string> StartupSoundBanks => _startupSoundbanks;
        
        // TODO: Fix this spaghetti code. ScriptableObjects cannot start Coroutines.
        // Should there be a separate BankLoader component?
        // Or just handle the startup soundbanks on the AudioManager.
        public void LoadStartupSoundbanks() =>
            AudioManager.Instance.StartCoroutine(Co_LoadStartupSoundbanks());
        
        private IEnumerator Co_LoadStartupSoundbanks()
        {
            foreach (var bank in _startupSoundbanks)
            {
                if (bank == "") continue;
                RuntimeManager.LoadBank(bank, true);
            }
        
            while (!RuntimeManager.HaveMasterBanksLoaded)
                yield return null;
        
            while (RuntimeManager.AnySampleDataLoading())
                yield return null;
        
            yield return new WaitForSeconds(_minimumLoadTime);
        
            MasterBanksLoaded?.Invoke();
            MasterBanksAreLoaded = true;
        }
    }

    public struct Soundbank
    {
        public string Name;
        [BankRef] public string Bank;
    }
}