using Cinemachine;
using PixelCrew.Creatures;
using UnityEngine;

namespace PixelCrew.Components.LVLManagment
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour
    {
        private void Start()
        {
            var vCam = GetComponent<CinemachineVirtualCamera>();
            vCam.Follow = FindObjectOfType<Hero>().transform;
        }
    }
}