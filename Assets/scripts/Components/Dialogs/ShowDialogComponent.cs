using Assets.scripts.Model.Data;
using Assets.scripts.Model.Definition;
using Assets.scripts.UI.HUD.Dialogs;
using System;
using UnityEngine;

namespace Assets.scripts.Components.Dialogs
{
   public  class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;

        private DialogBoxController _dialogBox;
        public void Show()
        {
            if (_dialogBox == null)
                _dialogBox = FindObjectOfType<DialogBoxController>();

            _dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDef def)
        {
            _external = def;
            Show();
        }

        public void Close()
        {
            _dialogBox.OnClose();
        }

        private DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}
