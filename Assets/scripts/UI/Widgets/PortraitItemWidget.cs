using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.Widgets
{
    public class PortraitItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _portrait;
        [SerializeField] private Animator _animator;
        public Image Portrait => _portrait;

        public readonly static int WiddingKey = Animator.StringToHash("Is-Wide");

        public void SetPortrait(Image portrait)
        {
            _portrait = portrait;
        }

        public void WidePortrait()
        {
            _animator.SetBool(WiddingKey, true);
        }

        public void ConstrictPortrait()
        {
            _animator.SetBool(WiddingKey, false);
        }
    }
}
