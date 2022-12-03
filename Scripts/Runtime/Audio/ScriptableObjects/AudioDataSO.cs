using UnityEngine;

namespace OCSFX.FMOD.AudioData
{
    public abstract class AudioDataSO : ScriptableObject
    {
        protected virtual void OnEnable()
        {
            AudioStatics.MasterBanksLoaded += OnMasterBanksLoaded;
        }

        protected virtual void OnDisable()
        {
            AudioStatics.MasterBanksLoaded -= OnMasterBanksLoaded;
        }

        protected virtual void OnMasterBanksLoaded() {}
    }
}
