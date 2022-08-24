using Assets.scripts.UI.HUD.Dialogs;
using UnityEngine;

namespace Assets.scripts.Components.Dialogs
{
    class ShowDialogBarComponent : MonoBehaviour
    {
        [SerializeField] private DialogBarData[] _external;

        private DialogBarController _dialogBar;
        public void Show()
        {
            if (_dialogBar == null)
                _dialogBar = GameObject.FindObjectOfType<DialogBarController>();

            _dialogBar.ShowDialog(Data);
        }

        private DialogBarData[] Data => _external;
    }
}
