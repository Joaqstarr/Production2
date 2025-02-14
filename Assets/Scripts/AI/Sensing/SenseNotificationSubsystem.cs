using UnityEngine;

namespace AI.Sensing
{
    public struct SenseNotificationContext
    {
        public enum NotificationType
        {
            None,
            Sound,
            Sight,
            Alarm
        }
        
        public NotificationType Type;
        public Vector3 Position;
        public float Strength;

        public SenseNotificationContext(NotificationType type, Vector3 position, float strength)
        {
            Type = type;
            Position = position;
            Strength = 0f;
        }
    }
    public class SenseNotificationSubsystem
    {
        
        public delegate void NotificationDelegate(SenseNotificationContext notificationContext);
        
        public static NotificationDelegate OnNotification;

        public static void TriggerSightNotifcation(Vector3 location)
        {
            SenseNotificationContext context = new SenseNotificationContext
            {
                Type = SenseNotificationContext.NotificationType.Sight,
                Position = location,
            };
            OnNotification?.Invoke(context);

        }
        
        public static void TriggerSoundNotification(Vector3 location, float volumeRange)
        {
            SenseNotificationContext context = new SenseNotificationContext
            {
                Type = SenseNotificationContext.NotificationType.Sound,
                Position = location,
                Strength = volumeRange
            };
            
            OnNotification?.Invoke(context);
        }

        public static void TriggerAlarmNotification(Vector3 location, float strength)
        {
            SenseNotificationContext context = new SenseNotificationContext
            {
                Type = SenseNotificationContext.NotificationType.Alarm,
                Position = location,
            };
            OnNotification?.Invoke(context);
        }
    }
}