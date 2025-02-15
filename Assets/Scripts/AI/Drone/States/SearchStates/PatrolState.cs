using UnityEngine;

namespace AI.Drone.States.SearchStates
{
    public class PatrolState: BaseState
    { 
        private SearchState _searchState;
        public PatrolState(DroneBrain drone, DroneStateMachineManager manager,  SearchState searchState) : base(drone, manager)
        {
            _searchState = searchState;
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            Transform nextPoint = GetRandomPatrolPoint();
            
            _drone.Agent.SetDestination(nextPoint.position);
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            if (_drone.Agent.remainingDistance <= _drone.Agent.stoppingDistance)
            {
                _searchState.SwitchToLookState();
            }
        }

        public Transform GetRandomPatrolPoint()
        {
            if (_drone.PatrolPoints.Length == 0)
            {
                Debug.LogWarning("No patrol points assigned!");
                return null;
            }

            int randomIndex = Random.Range(0, _drone.PatrolPoints.Length);
            return _drone.PatrolPoints[randomIndex];
        }
    }
}