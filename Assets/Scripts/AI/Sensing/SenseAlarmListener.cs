using UnityEngine;

namespace AI.Sensing
{
    public class SenseAlarmListener : SenseListenerBase
    {
        private readonly int _group;
        public SenseAlarmListener(int group) : base(SenseNotificationContext.NotificationType.Alarm)
        {
            _group = group;
        }

        protected override bool IsNotificationInRange(SenseNotificationContext notification)
        {
            return Mathf.Approximately(notification.Strength, _group);
        }
    }
}