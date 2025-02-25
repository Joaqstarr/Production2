using System;
using UnityEngine;

namespace AI.Sensing.Debug
{
    public class SightSenseTest : MonoBehaviour
    {
        private void Update()
        {
            if (transform != null)
            {
                SenseNotificationSubsystem.TriggerSightNotifcation(transform.position);
            }
        }
    }
}   