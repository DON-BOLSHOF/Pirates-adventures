﻿using System;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.HUD.Dialogs
{
    public class OptionalDialogController: MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private Text _contentText;
        [SerializeField] private OptionItemWidget _prefab;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _content.transform);
        }

        public void OnOptionSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            _content.SetActive(true);
            _contentText.text = data.DialogText;

            _dataGroup.SetData(data.Options);
        }
    }

    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }

    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}
