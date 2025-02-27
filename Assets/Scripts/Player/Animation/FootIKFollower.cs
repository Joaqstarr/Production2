using UnityEngine;

namespace Player.Animation
{
    public class FootIKFollower : StateMachineBehaviour
    {
        
        [Header("Settings")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _raycastDistance = 1.0f;
        [SerializeField] private float _footOffset = 0.1f;
        
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            
            
            // Left Foot IK
            AdjustFootTarget(animator, AvatarIKGoal.LeftFoot);
            // Right Foot IK
            AdjustFootTarget(animator, AvatarIKGoal.RightFoot);
        }
        
        private void AdjustFootTarget(Animator animator, AvatarIKGoal foot)
        {
            RaycastHit hit;
            
            Vector3 footPosition = animator.GetIKPosition(foot);

            
            Debug.DrawRay(footPosition + (Vector3.up * _raycastDistance/2), Vector3.down * _raycastDistance, Color.red);
            // Raycast to find the ground
            if (Physics.Raycast(footPosition + (Vector3.up * _raycastDistance/2), Vector3.down, out hit, _raycastDistance, _groundLayer))
            {
                Vector3 targetPosition = hit.point + Vector3.up * _footOffset;
                animator.SetIKPosition(foot, targetPosition);
                animator.SetIKPositionWeight(foot, 1);

                Quaternion targetRotation = Quaternion.LookRotation(animator.transform.forward, hit.normal);
                animator.SetIKRotation(foot, targetRotation);
                animator.SetIKRotationWeight(foot, 1);
            }
            else
            {
                animator.SetIKPositionWeight(foot, 0);
                animator.SetIKRotationWeight(foot, 0);
            }
        }
    }
}