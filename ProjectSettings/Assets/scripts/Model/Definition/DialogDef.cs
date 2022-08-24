using Assets.scripts.Model.Data;
using UnityEngine;

namespace Assets.scripts.Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/DialogDef", fileName ="Dialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;
    }
}
