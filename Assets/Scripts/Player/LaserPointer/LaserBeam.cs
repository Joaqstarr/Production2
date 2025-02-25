using UnityEngine;

namespace Player.LaserPointer
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserBeam : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                _lineRenderer.SetPosition(1, new Vector3(0, 0, hit.distance));
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