using System;
using FMODUnity;
using UnityEngine;

namespace OCSFX.FMOD
{
    public static class AudioStatics
    {
        public static Action MasterBanksLoaded = OnMasterBanksLoaded;
        public static bool MasterBanksAreLoaded { get; private set; } = false;

        private static void OnMasterBanksLoaded() => MasterBanksAreLoaded = true;

        public static void SetFMODParameterGlobal(string paramRef, float newValue)
        {
            RuntimeManager.StudioSystem.setParameterByName(paramRef, newValue);
        }
    }
}

