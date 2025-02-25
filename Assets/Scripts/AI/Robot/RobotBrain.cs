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


        private void OnEnable()
        {
            CatchCutscene.OnCaughtCutscneTriggered += OnCaughtCutscneTriggered;
        }

        private void OnDisable()
        {
            CatchCutscene.OnCaughtCutscneTriggered -= OnCaughtCutscneTriggered;

        }

        private void OnCaughtCutscneTriggered(Transform player)
        {
            Agent.enabled = false;
        }
    }
}