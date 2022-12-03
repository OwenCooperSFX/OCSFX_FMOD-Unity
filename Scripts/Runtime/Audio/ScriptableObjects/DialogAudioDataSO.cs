using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace OCSFX.FMOD.AudioData
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Dialog")]
    public class DialogAudioDataSO : AudioDataSO
    {
        [Space(8)]
        [SerializeField] private List<EventReference> _lines = new List<EventReference>();

        public EventReference GetLine(int index)
        {
            return _lines[index];
        }
    }
}