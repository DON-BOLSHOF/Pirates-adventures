using System;
using UnityEngine;

namespace Assets.scripts.Model.Data.Properties
{
    [Serializable]
    public class FloatPersistantProperty : PrefsPersistantProperty<float>
    {
        public FloatPersistantProperty(float defValue, string key):base(defValue, key)
        {
            Init();
        }

        protected override float Read(float defaultValue)
        {
            return PlayerPrefs.GetFloat(Key, defaultValue);
        }

        protected override void Write(float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
    }
}
