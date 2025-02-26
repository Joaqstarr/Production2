using AI.Sensing;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Mouse
{
    public class MouseBrain : MonoBehaviour
    {
        [SerializeField] private float _minDir = 1;
        private GenericDistanceListener _laserListener;

        private NavMeshAgent _agent;

        // Start is called before the first frame update
        void Start()
        {
            _laserListener = new GenericDistanceListener(SenseNotificationContext.NotificationType.Laser, 5, transform);
            _laserListener.OnNotificationReceived += OnLaserReceived;
            _agent = GetComponent<NavMeshAgent>();
        }


        private void OnLaserReceived(SenseNotificationContext notification)
        {
            Vector3 dir = notification.Position - transform.position;
            if (dir.AlmostZero())
            {
                dir = transform.forward;
                _agent.speed = 1;
            }
            else
            {
                _agent.speed = 5;
            }
            if (dir.sqrMagnitude < _minDir * _minDir)
            {
                dir = dir.normalized * _minDir;
            }
            
            
            _agent.SetDestination(transform.position + dir);
        }
    }
}