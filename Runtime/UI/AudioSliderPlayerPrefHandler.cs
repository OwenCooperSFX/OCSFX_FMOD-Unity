using UnityEngine;
using UnityEngine.UI;

namespace OCSFX.UI
{
    public class AudioSliderPlayerPrefHandler : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _defaultValue = 1.0f;
        [Tooltip("PlayerPref name inherits the name of the referenced slider unless overridden below.")]
        [SerializeField] private Overrides _overrides;
        private string _playerPrefName;
    
        public string PlayerPrefName => _playerPrefName;
    
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(SaveValue);
        }
    
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(SaveValue);
        }
    
        private void Start()
        {
            var savedValue = PlayerPrefs.GetFloat(_playerPrefName);
    
            _slider.value = PlayerPrefs.HasKey(_playerPrefName) ? savedValue : _defaultValue;
            
            SaveValue(_slider.value);
        }
    
        private void OnValidate()
        {
            if (!_slider) TryGetComponent(out _slider);
    
            _playerPrefName = _overrides.OverrideName ? _overrides.OverridePlayerPrefName : _slider.name;
        }
    
        private void SaveValue(float value)
        {
            PlayerPrefs.SetFloat(_playerPrefName, value);
        }
    
        [System.Serializable]
        private struct Overrides
        {
            [SerializeField] private bool _overrideName;
            [SerializeField] private string _overridePlayerPrefName;
            
            public bool OverrideName
            {
                get => _overrideName;
                set => _overrideName = value;
            }
            
            public string OverridePlayerPrefName
            {
                get => _overridePlayerPrefName;
                set => _overridePlayerPrefName = value;
            }
        }
    }
}