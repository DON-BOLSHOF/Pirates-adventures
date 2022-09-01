using UnityEngine;

namespace PixelCrew.Components
{
    public class ModifyHeathComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void ModifyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if(healthComponent != null)
            {
                healthComponent.ModifyHealth(_damage);
            }
        }
    }
}
