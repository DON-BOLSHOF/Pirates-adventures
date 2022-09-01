using System;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.HUD.Dialogs
{
    public class OptionItemWidget : MonoBehaviour, IItemRenderer<OptionData>
    {
        [SerializeField] private Text _label;
        [SerializeField] private SelectOption _onSelect;

        private OptionData _data;

        public void OnSelected()
        {
            _onSelect?.Invoke(_data);
        }

        public void SetData(OptionData data, int index)
        {
            _data = data;
            _label.text =(index+1).ToString() + ". "  + data.Text;
        }

        [Serializable]
        public class SelectOption: UnityEvent<OptionData> { }
    }
}
