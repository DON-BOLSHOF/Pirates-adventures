using UnityEngine;

namespace Assets.scripts.Model.Data.Properties
{
    class StringPersistantProperty : PrefsPersistantProperty<string>
    {
        public StringPersistantProperty(string value, string key) : base(value, key)
        {
            Init();
        }

        protected override string Read(string defaultValue)
        {
            return PlayerPrefs.GetString(Key, defaultValue);
        }

        protected override void Write(string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save(); 
        }
    }
}
