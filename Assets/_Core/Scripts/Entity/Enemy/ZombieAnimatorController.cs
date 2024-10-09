using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ZombieBehaviour))]
    public class ZombieAnimatorController : MonoBehaviour
    {
        private readonly int _movingAnimHash = Animator.StringToHash("Moving");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            GetComponent<ZombieBehaviour>().OnFollowStateChanged += UpdateMoveAnim;
        }

        private void UpdateMoveAnim(bool isMoving) => _animator.SetBool(_movingAnimHash, isMoving);
    }
}