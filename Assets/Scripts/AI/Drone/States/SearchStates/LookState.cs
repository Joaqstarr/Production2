using System.Collections;
using UnityEngine;
using World;

namespace AI.Drone.States.SearchStates
{
    public class LookState : BaseState
    {
        private SearchState _searchState;

        private float _previousAgentSpeed;
        public LookState(DroneBrain drone, DroneStateMachineManager manager,  SearchState searchState) : base(drone, manager)
        {
            _searchState = searchState;
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            _previousAgentSpeed = _drone.Agent.speed;
            if (_drone)
            {
                _drone.Agent.SetDestination(_drone.transform.position);
                
                _drone.Agent.speed = 3;
            }

        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            Vector3 dir = _drone._lookTransform.position - _drone.transform.position;

            if (CutsceneManager.IsInCutscene)
            {
                dir *= -1;
            }
            _drone.Agent.SetDestination(_drone.transform.position + dir.normalized);
            
            if (_searchState.TimeoutTimer <= 0)
            {
                _searchState.SwitchToLostState();;
            }
        }

        protected override void OnExitState()
        {
            base.OnExitState();
            _drone.Agent.speed = _previousAgentSpeed;

        }
    }
}