using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Model.Data.Properties
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
