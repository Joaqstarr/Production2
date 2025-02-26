using AI.Sensing;
using UnityEngine;

namespace Player.LaserPointer
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserBeam : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _laserHitMask;
        private LineRenderer _lineRenderer;
        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if(_lineRenderer.enabled == false) return;
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5000, _laserHitMask))
            {
                _lineRenderer.SetPosition(1, new Vector3(0, 0, hit.distance));
                
                SenseNotificationSubsystem.TriggerLaserNotification(hit.point);
            }
            else
            {
                _lineRenderer.SetPosition(1, new Vector3(0, 0, 5000));
            }
        }

        public void EnableBeam()
        {
            _lineRenderer.enabled = true;
            
        }

        public void DisableBeam()
        {
            _lineRenderer.enabled = false;
        }
    }
}