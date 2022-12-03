using UnityEngine;

namespace OCSFX.Generics
{
    [System.Serializable]
    public class KeyValuePair<TK, TV>
    {
        [SerializeField] TK _key;
        [SerializeField] TV _value;

        public TK Key
        {
            get => _key;
            set => _key = value;
        }

        public TV Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
