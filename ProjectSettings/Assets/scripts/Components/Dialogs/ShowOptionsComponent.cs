using Assets.scripts.UI.HUD.Dialogs;
using UnityEngine;

namespace Assets.scripts.Components.Dialogs
{
    class ShowOptionsComponent : MonoBehaviour
    {
        [SerializeField] private OptionDialogData _data;
        private OptionalDialogController _dialogBox;
        public void Show()
        {
            if(_dialogBox == null)
            {
                _dialogBox = FindObjectOfType<OptionalDialogController>();
            }

            _dialogBox.Show(_data);
        }
    }
}
