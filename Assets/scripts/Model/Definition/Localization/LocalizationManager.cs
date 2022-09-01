using System;
using System.Collections.Generic;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Definition.Localization
{
    public class LocalizationManager
    {
        public readonly static LocalizationManager I;

        private Dictionary<string, string> _localization;
        private StringPersistantProperty _currentLocale = new StringPersistantProperty("EN", "localization/current");
        public string LocaleKey => _currentLocale.Value;

        public event Action OnLocaleChanged;
        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(_currentLocale.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
             var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _localization = def.GetData();
            _currentLocale.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string selectedLocale)
        {
            LoadLocale(selectedLocale);
        }
    }
}
