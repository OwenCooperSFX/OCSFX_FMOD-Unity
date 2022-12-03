using System.Collections.Generic;
using UnityEngine;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Surfaces")]
    public class SurfacesAudioDataSO : AudioDataSO
    {
        [SerializeField] private List<OCSFX.Generics.KeyValuePair<AudioSurface, PhysicMaterial[]>> _surfaces = new();

        private readonly Dictionary<AudioSurface, PhysicMaterial[]> _dictionary = new();

        protected override void OnEnable()
        {
            RefreshDictionary();
        }

        public AudioSurface GetSurfaceType(PhysicMaterial physMat)
        {
            var returnSurfaceType = AudioSurface.Stone;
            
            foreach (var surface in _dictionary)
            {
                foreach (var physicMaterial in surface.Value)
                {
                    if (physMat.name.Contains(physicMaterial.name))
                    {
                        //Debug.Log(physicMaterial + " WAS FOUND");
                        returnSurfaceType = surface.Key;
                    }
                }
            }

            return returnSurfaceType;
        }

        private void RefreshDictionary()
        {
            _dictionary.Clear();
    
            foreach (var surface in _surfaces)
            {
                _dictionary.Add(surface.Key, surface.Value);
                var surfaceType = surface.Key;

                foreach (var entry in surface.Value)
                {
                    if (!entry) continue;
                    Debug.Log($"{surfaceType} | {entry.name}");
                }
            }
        }
    
        private void OnValidate()
        {
            RefreshDictionary();
        }
    }
}
