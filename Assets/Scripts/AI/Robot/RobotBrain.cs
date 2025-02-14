using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Robot
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RobotBrain : MonoBehaviour
    {
        public RobotStateMachineManager RobotStateMachine {get; private set;}

        public NavMeshAgent Agent {get; private set;}
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            RobotStateMachine = new RobotStateMachineManager(this);
        }
    }
}