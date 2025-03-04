namespace AI.Sensing
{
    public abstract class SenseListenerBase
    {
        private SenseNotificationContext.NotificationType NotificationType;
        
        public delegate void NotificationReceivedDelegate(SenseNotificationContext notification);
        public NotificationReceivedDelegate OnNotificationReceived;
        
        public SenseListenerBase(SenseNotificationContext.NotificationType type)
        {
            NotificationType = type;

            SenseNotificationSubsystem.OnNotification += OnNotificationReceivedHandler;
            OnNotificationReceived = null;
        }

        ~SenseListenerBase()
        {
            SenseNotificationSubsystem.OnNotification -= OnNotificationReceivedHandler;
        }

        private void OnNotificationReceivedHandler(SenseNotificationContext notification)
        {
            if (notification.Type == NotificationType && IsNotificationInRange(notification))
            {
                OnNotificationReceived?.Invoke(notification);
            }
        }

        protected abstract bool IsNotificationInRange(SenseNotificationContext notification);
        
        
        public static SenseListenerBase operator +(SenseListenerBase listener, NotificationReceivedDelegate handler)
        {
            listener.OnNotificationReceived += handler;
            return listener;
        }

        // Overload - operator to remove a delegate
        public static SenseListenerBase operator -(SenseListenerBase listener, NotificationReceivedDelegate handler)
        {
            listener.OnNotificationReceived -= handler;
            return listener;
        }
    }
}