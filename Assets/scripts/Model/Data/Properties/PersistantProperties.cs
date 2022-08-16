namespace Assets.scripts.Model.Data.Properties
{
    public abstract class PersistantProperties<TPropertyType> : ObservableProperty<TPropertyType>
    {
        protected TPropertyType _stored;

        private TPropertyType _default;

        public PersistantProperties(TPropertyType defaultValue)
        {
            _default = defaultValue;
        }

        public override TPropertyType Value
        {
            get => _value;
            set
            {
                base.Value = value;
                Write(value);
                _stored = _value = value;
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
