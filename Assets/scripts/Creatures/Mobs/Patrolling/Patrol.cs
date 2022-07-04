using System.Collections;
using UnityEngine;

namespace Assets.scripts.Creatures.Mobs.Patrolling
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
