using System;
using PixelCrew.UI.HUD.Dialogs;
using UnityEngine;

namespace PixelCrew.Components.Dialogs
{
    class ShowDialogBarComponent : MonoBehaviour
    {
        [SerializeField] private DialogBarData[] _external;

        private DialogBarController _dialogBar; 

        public void Show()
        {
            if (_dialogBar == null)
                _dialogBar = GameObject.FindObjectOfType<DialogBarController>();

            _dialogBar.ShowDialog(Data, OnEnd);
        }

        private void OnEnd()
        {
            Destroy(gameObject);
        }

        public void Close()
        {
            _dialogBar.CloseEarly();
        }

        private DialogBarData[] Data => _external;
    }
}
