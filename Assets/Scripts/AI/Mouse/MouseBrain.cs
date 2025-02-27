using AI.Sensing;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace AI.Mouse
{
    public class MouseBrain : MonoBehaviour
    {
        [SerializeField] private float _minDist = 0.25f;
        private GenericDistanceListener _laserListener;

        private NavMeshAgent _agent;

        Vector3 _targPosition;

        // Start is called before the first frame update
        void Start()
        {
            _targPosition = transform.position;
            _laserListener = new GenericDistanceListener(SenseNotificationContext.NotificationType.Laser, 5, transform);
            _laserListener.OnNotificationReceived += OnLaserReceived;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            Vector3 dir = _targPosition - transform.position;
            if (dir.AlmostZero() && _agent.velocity.AlmostZero())
            {
                dir = transform.forward +transform.right;
                _agent.speed = 1;
            }
            else
            {
                _agent.speed = 5;
            }
            if (dir.sqrMagnitude < _minDist * _minDist)
            {
                dir = dir.normalized * _minDist;
            }


            _agent.SetDestination(transform.position + dir);
        }

        private void OnLaserReceived(SenseNotificationContext notification)
        {
            _targPosition = notification.Position;
        }
        
        public void OnStomped()
        {
            Destroy(gameObject);

        }
        public void OnPrepareStomped()
        {
            _agent.isStopped = true;
            enabled = false;
        }
    }
}