using System.Collections;
using UnityEngine;

namespace Assets.scripts.Creatures.Patrolling
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
