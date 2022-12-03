#if UNITY_EDITOR
using System.Collections.Generic;
using FMODUnity;
using UnityEditor;
using UnityEngine;

namespace OCSFX.FMOD
{
    public static class EditorMenuItems
    {
        [MenuItem("OCSFX/AudioManager")]
        public static void AddAudioManagerPrefabToCurrentScene()
        {
            var audioManager = Object.FindObjectOfType<AudioManager>();

            if (audioManager)
            {
                Debug.LogWarning("AudioManager already exists in the scene.");
            }
            else
            {
                var prefab = AssetDatabase.LoadAssetAtPath("Assets/Database/OCSFX_FMOD/Prefabs/Audio/AudioManager.prefab", typeof(GameObject)) as GameObject;
                PrefabUtility.InstantiatePrefab(prefab);
            }
        }

        [MenuItem("OCSFX/FMOD Listener(s)")]
        public static void SetFmodAudioListeners()
        {
            var audioListeners = Object.FindObjectsOfType<AudioListener>();

            foreach (var listener in audioListeners)
            {
                var owner = listener.gameObject;
                if (!owner.TryGetComponent<StudioListener>(out var fmodListener))
                    fmodListener = owner.AddComponent<StudioListener>();

                Object.DestroyImmediate(listener);
            }
            
            DestroyDuplicateListeners();
        }

        private static void DestroyDuplicateListeners()
        {
            var fmodListeners = Object.FindObjectsOfType<StudioListener>();

            if (fmodListeners.Length < 1) return;

            var owners = new HashSet<GameObject>();

            foreach (var listener in fmodListeners)
            {
                owners.Add(listener.gameObject);
            }

            foreach (var obj in owners)
            {
                var listeners = obj.GetComponents<StudioListener>();

                if (listeners.Length <= 1) continue;
                
                for (int i = 1; i < listeners.Length; i++)
                {
                    Object.DestroyImmediate(listeners[i]);
                }
            }
        }
    }
}
#endif
