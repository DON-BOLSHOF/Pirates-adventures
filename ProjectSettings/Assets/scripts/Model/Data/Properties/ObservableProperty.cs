using Assets.scripts.Utils.Disposables;
using System;
using UnityEngine;

namespace Assets.scripts.Model.Data.Properties
{
    [Serializable]
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;
        public IDisposable Subscribe(OnPropertyChanged call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }
        
        public IDisposable SubscribeAndInvoke(OnPropertyChanged call)
        {
            OnChanged += call;
            call(_value,_value);
            return new ActionDisposable(() => OnChanged -= call);
        }

        public virtual TPropertyType Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value))
                    return;

                var oldValue = _value;
                _value = value;

                InvokeChangedEvent(_value, oldValue);
            }
        }

        protected void InvokeChangedEvent(TPropertyType newValue, TPropertyType oldValue)
        {
            OnChanged?.Invoke(newValue, oldValue);
        }
    }
}
