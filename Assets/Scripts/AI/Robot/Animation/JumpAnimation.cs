using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Robot.Animation
{
    public class JumpAnimation : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;

        private bool _hasStartedJump = false;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!_hasStartedJump && _agent.isOnOffMeshLink)
            {
                StartCoroutine(JumpCoroutine());
            }
        }

        IEnumerator JumpCoroutine()
        {
            _hasStartedJump = true;
            _agent.autoTraverseOffMeshLink = false;
            _animator.SetTrigger("StartJump");

            while ( Vector3.Distance(_agent.currentOffMeshLinkData.endPos, transform.position) > 0.1f)
            {
                yield return null;
            }

            _agent.isStopped = true;   
            _animator.SetTrigger("LandJump");

        }

        void OnInAir()
        {
            _agent.autoTraverseOffMeshLink = true;
        }
        public void OnLand()
        {
            _agent.CompleteOffMeshLink();
            _agent.isStopped = false;   

            _hasStartedJump = false;
        }
    }
}