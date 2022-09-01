using UnityEngine;
using System;
using System.Linq;

namespace PixelCrew.Components
{
    class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
           var spawner =  _spawners.FirstOrDefault(x => x.id == id);
            spawner?.Component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            public string id;
            public SpawnComponent Component;
        }
    }
}
