using Assets.scripts.UI.HUD.Dialogs;
using UnityEngine;

namespace Assets.scripts.Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/DialogBarDef", fileName ="Speech")]
    class DialogBarDef : ScriptableObject
    {
        [SerializeField]private DialogBarData _speech;

        public DialogBarData Speech => _speech;
    }
}
