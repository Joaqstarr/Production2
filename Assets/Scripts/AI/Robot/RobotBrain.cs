using System;
using UnityEngine;
using UnityEngine.AI;
using AI.Robot.Animation;

namespace AI.Robot
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RobotBrain : MonoBehaviour
    {
        public RobotStateMachineManager RobotStateMachine {get; private set;}

        [field: SerializeField] public int AlarmGroupID { get; private set; } = 0;
        public NavMeshAgent Agent {get; private set;}
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            RobotStateMachine = new RobotStateMachineManager(this);
        }

        ~RobotBrain()
        {
            RobotStateMachine = null;
        }

        private void OnEnable()
        {
            CatchCutscene.OnCaughtCutscneTriggered += OnCaughtCutsceneTriggered;
        }

        private void OnDisable()
        {
            CatchCutscene.OnCaughtCutscneTriggered -= OnCaughtCutsceneTriggered;

        }


        private void OnCaughtCutsceneTriggered(Transform player)
        {

            if (Agent.enabled)
            {
                Agent.isStopped = true;
            }
            Agent.enabled = false;
            enabled = false;
        }
    }
}