using AI.Sensing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace World
{
    public class MouseSpawner : MonoBehaviour
    {
        private GenericDistanceListener _laserListener;
        [SerializeField] private NavMeshAgent _mousePrefab;
        [SerializeField] private float _mouseJumpDist = 2;
        [SerializeField] private float _mouseJumpHeight = 2;
        [SerializeField] private float _mouseJumpTime = 1;
        [SerializeField] private float _laserSenseRange = 3;
        [SerializeField] private Transform _rayCastPoint;
        [SerializeField] private LayerMask _environmentLayerMask;
        private NavMeshAgent _mouse;
        
        private void Start()
        {
            _laserListener = new GenericDistanceListener(SenseNotificationContext.NotificationType.Laser, _laserSenseRange, transform);
            _laserListener.OnNotificationReceived += OnNotificationReceived;
        }

        private void OnNotificationReceived(SenseNotificationContext notification)
        {
            if (_mouse ==null && LineOfSightCheck(notification.Position))
            {
                NavMeshQueryFilter filter = new NavMeshQueryFilter();
                filter.agentTypeID = _mousePrefab.agentTypeID; // Ensure the agent type ID matches the prefab
                filter.areaMask = NavMesh.AllAreas; // Ensure the area mask matches the prefab

                Vector3 dir = (notification.Position - transform.position).normalized;
                Vector3 mouseJumpRawPos = transform.position + (dir * _mouseJumpDist);
                NavMeshHit hit;

                // Increase the max distance to ensure it finds a valid position
                float maxDistance = 4f;

                if (NavMesh.SamplePosition(mouseJumpRawPos, out hit, maxDistance, filter))
                {
                    _mouse = Instantiate(_mousePrefab, transform.position, Quaternion.identity);
                    _mouse.enabled = false;
                    _mouse.transform.forward = dir;

                    _mouse.transform.DOJump(hit.position, _mouseJumpHeight, 1, _mouseJumpTime).OnComplete(() =>
                    {
                        _mouse.enabled = true;
                    });
                }
                else
                {
                   // Debug.LogWarning("NavMesh.SamplePosition failed to find a valid position.");
                }

            }
        }

        private bool LineOfSightCheck(Vector3 pos)
        {
            RaycastHit hit;
            Vector3 direction = (pos - _rayCastPoint.position).normalized;
            if (Physics.Raycast(_rayCastPoint.position, direction, out hit, 6, _environmentLayerMask))
            {
                float dist = Vector3.Distance(hit.point, pos);
                // Check if the hit object is the target position
                if (dist < 0.5f)
                {
                    return true;
                }

                return false;
            }
            
            
            return true;
        }
    }
}
