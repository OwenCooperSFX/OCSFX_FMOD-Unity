using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using static OCSFX.FMOD.AudioStatics;

namespace OCSFX.FMOD
{
    [CreateAssetMenu(menuName = "OCSFX.FMOD/Audio Data/Dialog Audio Data")]
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