using UnityEngine;

namespace AI.Sensing
{
    public class SightSenseListener : SenseListenerBase
    {
        private Transform _transform;
        private GameObject _gameObject;
        private float _sightRange;
        private LayerMask _layerMask;
        
        public SightSenseListener(GameObject seePoint, float sightRange, LayerMask layerMask) : base(SenseNotificationContext.NotificationType.Sight)
        {
            _transform = seePoint.transform;
            _gameObject = seePoint;
            _sightRange = sightRange;
            _layerMask = layerMask;
        }

        protected override bool IsNotificationInRange(SenseNotificationContext notification)
        {
            if (_transform == null) return false;

            if (Vector3.Distance(_transform.position, notification.Position) > _sightRange) return false;
            
            RaycastHit hit;
            
            Vector3 dir = notification.Position - _transform.position;
            if (Physics.Raycast(_transform.position, dir, out hit, _sightRange))
            {
                if (_layerMask == (_layerMask | 1 << hit.collider.gameObject.layer))
                {
                    return false;
                }
                //todo: layer logic
                return true;
            }

            return true;
        }
    }
}