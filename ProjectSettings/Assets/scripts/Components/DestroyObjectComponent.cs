using UnityEngine;
namespace Assets.scripts.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void OnDestroy()
        {
            Destroy(_objectToDestroy);
        }
    }
}