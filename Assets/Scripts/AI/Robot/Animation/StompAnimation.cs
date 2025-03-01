using System;
using UnityEngine;

namespace AI.Robot.Animation
{
    public class StompAnimation : MonoBehaviour
    {
        private bool _isStomping = false;
        
        [SerializeField]
        private Vector3 _stompOffset = new Vector3(-1, 0, 0.44969f);

        [SerializeField] private float _adjustmentSpeed = 5;

        private Vector3 _targetPosition;
        private Transform _stompTarget;
        private Animator _stompAnimator;
        private UnityEngine.AI.NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _stompAnimator = GetComponent<Animator>();
        }

        public void SetStompTarget(Transform targ)
        {
            _stompTarget = targ;
        }
        public void OnStompStart()
        {
            if (_stompTarget)
            {
                _agent.isStopped = true;
                _agent.enabled = false;
                _isStomping = true;

                Vector3 dir = _stompTarget.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                Vector3 worldOffset = targetRotation * _stompOffset;



                _targetPosition = _stompTarget.transform.position - worldOffset;

                transform.forward = (_targetPosition - transform.position).normalized;

                _stompTarget.SendMessage("OnPrepareStomped", SendMessageOptions.DontRequireReceiver);

                transform.position = _targetPosition;

            }
            else
            {
                _stompAnimator.SetTrigger("CancelStomp");

            }
        }

        public void OnStompHit()
        {
            if (_stompTarget)
            {
                _stompTarget.SendMessage("OnStomped", SendMessageOptions.DontRequireReceiver);
                
            }
        }
        public void OnStompEnd()
        {
            _isStomping = false;
            _agent.enabled = true;
            _agent.isStopped = false;
        }

        private void Update()
        {
            if (_isStomping)
            {
                //transform.position = Vector3.Lerp(transform.position, _targetPosition, _adjustmentSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isStomping && other.CompareTag("Stompable"))
            {
                SetStompTarget(other.transform);
                _stompAnimator.SetTrigger("Stomp");
            }
        }
    }
}