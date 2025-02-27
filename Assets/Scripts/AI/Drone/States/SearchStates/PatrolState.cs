using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace AI.Drone.States.SearchStates
{
    public class PatrolState : BaseState
    {
        private SearchState _searchState;
        private Transform _lookTransform;
        private float _sweepDuration = 4f; // Duration of each sweep
        private float _radius = 3f; // Radius of the circular movement
        private float _angle = 0;
        private Coroutine _sweepCoroutine;
        
        float _amplitude = 1f;
        float _frequency = 1f;

        public PatrolState(DroneBrain drone, DroneStateMachineManager manager, SearchState searchState) : base(drone, manager)
        {
            _searchState = searchState;
            _lookTransform = drone._lookTransform;
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

            _angle += (360f / _sweepDuration) * Time.deltaTime;
            if (_angle >= 360f) _angle -= 360f;

            float sinedRadius = _radius + (Mathf.Sin(Time.time * _frequency) * _amplitude);
            
            float radians = _angle * Mathf.Deg2Rad;
            Vector3 targetPosition = _drone.transform.position + new Vector3(Mathf.Cos(radians) * sinedRadius, 0, Mathf.Sin(radians) * sinedRadius);
            _lookTransform.position = targetPosition;
            
            
            if (_drone.Agent.remainingDistance <= _drone.Agent.stoppingDistance)
            {
                _searchState.SwitchToLookState();
            }
        }




        protected override void OnExitState()
        {
            base.OnExitState();
            if (_sweepCoroutine != null)
            {
                _drone.StopCoroutine(_sweepCoroutine);
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