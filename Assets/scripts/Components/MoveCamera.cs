using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private static readonly int PushKey = Animator.StringToHash("Push");

    public void MoveTheCamera()
    {
        _animator.SetTrigger(PushKey); 
    }
}
