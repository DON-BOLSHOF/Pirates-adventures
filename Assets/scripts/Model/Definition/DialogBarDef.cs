using PixelCrew.UI.HUD.Dialogs;
using UnityEngine;

namespace PixelCrew.Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/DialogBarDef", fileName ="Speech")]
    class DialogBarDef : ScriptableObject
    {
        [SerializeField]private DialogBarData _speech;

        public DialogBarData Speech => _speech;
    }
}
