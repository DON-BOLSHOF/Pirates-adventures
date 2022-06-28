using UnityEngine;
using UnityEngine.Events;

namespace Assets.scripts.Components
{
    [System.Serializable]
    public class State
    {
        public string name;
        public bool allowsNext;
        public bool loop;
        public Sprite[] sprites;

        public UnityEvent onComplete;
    }
}