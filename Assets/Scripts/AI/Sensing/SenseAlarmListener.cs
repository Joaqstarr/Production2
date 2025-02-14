namespace AI.Sensing
{
    public class SenseAlarmListener : SenseListenerBase
    {
        public SenseAlarmListener() : base(SenseNotificationContext.NotificationType.Alarm)
        {
            
        }

        protected override bool IsNotificationInRange(SenseNotificationContext notification)
        {
            return true;
        }
    }
}