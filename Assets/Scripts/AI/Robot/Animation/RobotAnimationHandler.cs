using UnityEngine;
using UnityEngine.AI;
using World;

namespace AI.Robot.Animation
{
    public class RobotAnimationHandler : MonoBehaviour
    {
        private static readonly int SpeedProperty = Animator.StringToHash("Speed");
        private Animator _animator;
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }




        private void Update()
        {
            _animator.SetFloat(SpeedProperty, _agent.velocity.sqrMagnitude);
        }
    }
}