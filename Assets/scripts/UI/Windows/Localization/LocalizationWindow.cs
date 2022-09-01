using System.Collections.Generic;
using PixelCrew.Model.Definition.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Windows.Localization
{
    class LocalizationWindow : AnimatedWindow
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LocaleItemWidget _prefab;

        private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

        private string[] _supportedLocales = new[] { "EN", "RU" };

        protected override void Start()
        {
            base.Start();
            _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);

            _dataGroup.SetData(ComposeData());
        }
        private IList<LocaleInfo> ComposeData()
        {
            var data = new List<LocaleInfo>();

            foreach(var locale in _supportedLocales)
            {
                data.Add(new LocaleInfo { LocaleId = locale }) ;
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.I.SetLocale(selectedLocale);
        }
    }
}
