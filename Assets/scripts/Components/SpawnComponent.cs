using Assets.scripts.Utils;
using System;
using UnityEngine;


namespace Assets.scripts.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instance = SpawnUtils.Spawn(_prefab, _target.position);

            var scale = _target.lossyScale;
            instance.transform.localScale = scale;
            instance.SetActive(true);
        }

        internal void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    }
}
