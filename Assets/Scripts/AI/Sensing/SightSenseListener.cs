using UnityEngine;

namespace AI.Sensing
{
    public class SightSenseListener : SenseListenerBase
    {
        private Transform _transform;
        private GameObject _gameObject;
        private float _sightRange;
        
        public SightSenseListener(GameObject seePoint, float sightRange) : base(SenseNotificationContext.NotificationType.Sight)
        {
            _transform = seePoint.transform;
            _gameObject = seePoint;
            _sightRange = sightRange;
        }

        protected override bool IsNotificationInRange(SenseNotificationContext notification)
        {
            if (Vector3.Distance(_transform.position, notification.Position) > _sightRange) return false;
            
            RaycastHit hit;
            
            Vector3 dir = notification.Position - _transform.position;
            if (Physics.Raycast(_transform.position, dir, out hit, _sightRange))
            {
                //todo: layer logic
                return true;
            }

            return true;
        }
    }
}