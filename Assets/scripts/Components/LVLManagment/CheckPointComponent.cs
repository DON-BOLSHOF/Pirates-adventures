using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.LVLManagment
{
    [RequireComponent(typeof(SpawnComponent))]
     public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        public string Id => _id;

        [Space]
        [SerializeField] private UnityEvent _setOnChecked;
        [SerializeField] private UnityEvent _setUnChecked;

        [SerializeField] private SpawnComponent _heroSpawner;
        
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            if (_session.IsChecked(_id))
                _setOnChecked?.Invoke();
            else
                _setUnChecked?.Invoke();
        }

        public void Check()
        {
            _session.SetChecked(_id);
            _setOnChecked?.Invoke();
        }

        public void SpawnHero()
        {
            _heroSpawner.Spawn();
        }
    }
}
