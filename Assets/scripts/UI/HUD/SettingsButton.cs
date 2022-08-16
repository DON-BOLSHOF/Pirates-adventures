using UnityEngine;

namespace Assets.scripts.UI.HUD
{
    class SettingsButton : MonoBehaviour
    {
        public void OnShowSessionMenu()
        {
            var window = Resources.Load<GameObject>("UI/SessionMenu");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }
    }
}
