using UnityEngine;
using OCSFX.FMOD.AudioData;

namespace OCSFX.FMOD
{
    public class FMODReverbZone : MonoBehaviour
    {
        [SerializeField] private SnapshotsAudioDataSO _snapshotsAudioData;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ReverbType _reverbType;

        private int _overlapCount;
    
        private void OnDisable()
        {
            _overlapCount = 0;
            _snapshotsAudioData.SetReverbSnapshot(_reverbType, false);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if ((_layerMask.value & (1 << other.gameObject.layer)) == 0) return;
        
            if (_overlapCount < 1)
                _snapshotsAudioData.SetReverbSnapshot(_reverbType, true);
        
            _overlapCount++;
        }
    
        private void OnTriggerExit(Collider other)
        {
            if ((_layerMask.value & (1 << other.gameObject.layer)) == 0) return;

            _overlapCount--;
        
            if (_overlapCount < 1)
            {
                _snapshotsAudioData.SetReverbSnapshot(_reverbType, false);
            }
        }

        private void OnValidate()
        {
            var colliders = GetComponents<Collider>();

            if (colliders.Length <= 0) return;
        
            foreach (var colliderComp in colliders)
            {
                colliderComp.isTrigger = true;
            }
        }
    }   
}
