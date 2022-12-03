using UnityEngine;
using OCSFX.FMOD.AudioData;

namespace OCSFX.FMOD
{
    public class FMODAmbientZone : MonoBehaviour
    {
        [SerializeField] private AmbienceAudioDataSO _ambienceAudioData;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _ambienceIndex;

        private int _overlapCount;

        private void OnDisable()
        {
            _overlapCount = 0;
            _ambienceAudioData.StopAmbience(_ambienceIndex);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_layerMask.value & (1 << other.gameObject.layer)) == 0) return;

            if (_overlapCount < 1)
                _ambienceAudioData.PlayAmbience(_ambienceIndex);
        
            _overlapCount++;
        }

        private void OnTriggerExit(Collider other)
        {
            if ((_layerMask.value & (1 << other.gameObject.layer)) == 0) return;

            _overlapCount--;
        
            if (_overlapCount < 1)
            {
                _ambienceAudioData.StopAmbience(_ambienceIndex);
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
