using System.Collections;
using UnityEngine;

namespace AI.Drone.States.SearchStates
{
    public class LookState : BaseState
    {
        private SearchState _searchState;

        public LookState(DroneBrain drone, DroneStateMachineManager manager,  SearchState searchState) : base(drone, manager)
        {
            _searchState = searchState;
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();

            _drone.Agent.SetDestination(_drone.transform.position);

        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            if (_searchState.TimeoutTimer <= 0)
            {
                _searchState.SwitchToLostState();;
            }
        }
    }
}