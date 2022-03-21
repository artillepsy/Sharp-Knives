using Unity.Notifications.Android;
using UnityEngine;

namespace Management
{
    public class NotificationsManager : MonoBehaviour
    {
        [SerializeField] private string title;
        [SerializeField] private string text;
        [SerializeField] private int sheduleHours;
        [SerializeField] private int sheduleMinutes ;
        [SerializeField] private int sheduleSeconds = 15;
        private float _checkTimeInSeconds = 10f;
        private float MinToSec => sheduleMinutes / 60f;
        private float HourToSec => sheduleHours / 3600f;

        private void Start() => InvokeRepeating(nameof(SheduleNotifications),0, _checkTimeInSeconds);
        private void SheduleNotifications()
        {
            AndroidNotificationChannel channel = new AndroidNotificationChannel()
            {
                Id = "Custom",
                Name = "Default channel",
                Importance = Importance.Default,
                Description = "None"
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            AndroidNotification notification = new AndroidNotification()
            {
                Title = title,
                Text = text,
                SmallIcon = "small",
                LargeIcon = "large",
                ShowTimestamp = true,
                FireTime = System.DateTime.Now.AddSeconds(HourToSec + MinToSec + sheduleSeconds)
            };

            var id = AndroidNotificationCenter.SendNotification(notification, channel.Id);
            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) != NotificationStatus.Scheduled) return;
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, channel.Id);
        }
    }
}