using AI.Sensing;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Mouse
{
    public class MouseBrain : MonoBehaviour
    {
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
            _agent.SetDestination(notification.Position);
        }
    }
}