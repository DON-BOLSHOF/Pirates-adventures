using System;
using UnityEngine;

namespace Assets.scripts.Model
{
    [Serializable]
    public class Cooldown
    {
        public float cooldownTime=2;

        private float lastChangedTime;

        public void Reset()
        {
            lastChangedTime = Time.time + cooldownTime;
        }

        public bool IsReady => Time.time > lastChangedTime;
    }
}
