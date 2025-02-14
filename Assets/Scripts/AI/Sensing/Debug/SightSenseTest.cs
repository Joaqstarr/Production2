using System;
using UnityEngine;

namespace AI.Sensing.Debug
{
    public class SightSenseTest : MonoBehaviour
    {
        private void Update()
        {
            SenseNotificationSubsystem.TriggerSightNotifcation(transform.position);
        }
    }
}