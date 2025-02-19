using UnityEngine;

namespace AI.Sensing
{
    public class GenericDistanceListener : SenseListenerBase
    {
        private readonly float _range;
        private readonly Transform _transform;
        public GenericDistanceListener(SenseNotificationContext.NotificationType type, float range, Transform transform) : base(type)
        {
            _range = range;
            _transform = transform;
        }

        protected override bool IsNotificationInRange(SenseNotificationContext notification)
        {
            return Vector3.Distance(notification.Position, _transform.position) < _range;
        }
    }
}