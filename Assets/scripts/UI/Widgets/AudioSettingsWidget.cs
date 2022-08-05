using Assets.scripts.Model.Data.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.Widgets
{
    class AudioSettingsWidget: MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;

        private FloatPersistantProperty _model;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }

        public void SetModel(FloatPersistantProperty model)
        {
            _model = model;

            _slider.normalizedValue = model.Value;

            OnValueChanged(model.Value, model.Value);

            model.OnChanged += OnValueChanged;
        }

        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.Round(newValue * 100);
            _text.text = textValue.ToString();
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            _model.OnChanged -= OnValueChanged;
        }
    }
}
