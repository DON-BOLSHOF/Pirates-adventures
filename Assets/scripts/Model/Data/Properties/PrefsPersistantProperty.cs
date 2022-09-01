namespace PixelCrew.Model.Data.Properties
{
    public abstract class PrefsPersistantProperty<TPropertyType> : PersistantProperties<TPropertyType>
    {
        protected string Key;

        protected PrefsPersistantProperty(TPropertyType defaultValue, string key):base(defaultValue)
        {
            Key = key;
        }
    }
}
