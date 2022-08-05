using System;
using UnityEngine;

namespace Assets.scripts.Model.Data.Properties
{
    [Serializable]
    public abstract class PersistantProperties<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;
        protected TPropertyType _stored;

        private TPropertyType _default;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;

        public PersistantProperties(TPropertyType defaultValue)
        {
            _default = defaultValue;
        }

        public TPropertyType Value
        {
            get => _value;
            set
            {
                if (_stored.Equals(value))
                    return;

                var oldValue = _value;
                Write(value);
                _stored = _value = value;

                OnChanged?.Invoke(value, oldValue); 
            }
        }

        protected void Init()
        {
            _stored = _value = Read(_default);
        }

        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);

        public void Validate()
        {
            Value = _value;
        }
    }
}
