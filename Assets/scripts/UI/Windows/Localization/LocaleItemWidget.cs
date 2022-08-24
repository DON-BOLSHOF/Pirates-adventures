using Assets.scripts.Model.Definition.Localization;
using Assets.scripts.UI.Widgets;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.scripts.UI.Windows.Localization
{
    class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelected;

        private LocaleInfo _data;

        private void Start()
        {
            LocalizationManager.I.OnLocaleChanged += UpdateSelection;
        }

        public void SetData(LocaleInfo localeKey, int index)
        {
            _data = localeKey;

            UpdateSelection();
            _text.text = localeKey.LocaleId;
        }

        public void UpdateSelection()
        {
            _selector.SetActive(LocalizationManager.I.LocaleKey == _data.LocaleId);
        }

        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocaleId);
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocaleChanged -= UpdateSelection;
        }
    }

    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {
    }

    public class LocaleInfo
    {
        public string LocaleId;
    }
}
