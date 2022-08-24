using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.Widgets
{
    class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}
