using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEngine;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Soundbanks Audio Data")]
    public class SoundbanksAudioDataSO : AudioDataSO
    {
        [Header("Startup Banks")]
        [BankRef] [SerializeField] private List<string> _startupBanks = new List<string>();
        [SerializeField] [Tooltip("A minimum added delay for safety.")] [Range(0f, 1f)]
        private float _minimumLoadTime = 0.1f;

        [Header("Runtime Banks")]
        [SerializeField] private List<NamedBank> _banks = new List<NamedBank>();

        private readonly Dictionary<string, string> _bankDictionary = new Dictionary<string, string>();

        public List<string> StartupBanks => _startupBanks;
        public List<NamedBank> RuntimeBanks => _banks;

        // TODO: Fix this spaghetti code. ScriptableObjects cannot start Coroutines.
        // Should there be a separate BankLoader component?
        // Or just handle the startup soundbanks on the AudioManager.
        public void LoadStartupSoundbanks() =>
            AudioManager.Instance.StartCoroutine(Co_LoadStartupSoundbanks());
        
        private IEnumerator Co_LoadStartupSoundbanks()
        {
            foreach (var bank in _startupBanks)
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

        public void LoadBank(string bank) => RuntimeManager.LoadBank(bank,true);

        public void UnloadBank(string bank) => RuntimeManager.UnloadBank(bank);
        
        public void LoadBankByName(string bankName) => RuntimeManager.LoadBank(_bankDictionary[bankName], true);
        
        public void UnloadBankByName(string bankName) => RuntimeManager.UnloadBank(_bankDictionary[bankName]);

        private void OnValidate()
        {
            foreach (var entry in _banks)
            {
                _bankDictionary.TryAdd(entry.Name, entry.Bank);
            }
            
            _bankDictionary.TrimExcess();
        }
    }

    [Serializable]
    public struct NamedBank
    {
        public string Name;
        [BankRef] public string Bank;
    }
}