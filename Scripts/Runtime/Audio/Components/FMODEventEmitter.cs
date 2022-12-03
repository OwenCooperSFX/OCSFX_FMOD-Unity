using System.Collections;
using FMODUnity;

namespace OCSFX.FMOD
{
    public class FMODEventEmitter : StudioEventEmitter
    {
        protected override void HandleGameEvent(EmitterGameEvent gameEvent)
        {
            if (!AudioStatics.MasterBanksAreLoaded)
            {
                StartCoroutine(AwaitBanksLoaded(gameEvent));
            }
            else
            {
                base.HandleGameEvent(gameEvent);
            }
        }

        private IEnumerator AwaitBanksLoaded(EmitterGameEvent gameEvent)
        {
            while (!AudioStatics.MasterBanksAreLoaded)
                yield return null;
            
            base.HandleGameEvent(gameEvent);
        }
    }
}
