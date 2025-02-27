using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace AI.Drone.States.SearchStates
{

    public class LostState : BaseState
    {
        private SearchState _searchState;
        private Transform _lookTransform;
        private int _moveCount = 3; // Number of random movements
        private float _moveDuration = 0.5f; // Duration of each movement
        private float _waitTime = 2f; // Duration of each movement

        private float _searchDistance = 5;
        public LostState(DroneBrain drone, DroneStateMachineManager manager, SearchState searchState) : base(drone,
            manager)
        {
            _searchState = searchState;
            _lookTransform = drone._lookTransform;
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            _drone.StartCoroutine(MoveLookTransformRandomly());
        }

        private IEnumerator MoveLookTransformRandomly()
        {
            for (int i = 0; i < _moveCount; i++)
            {
                Vector3 randomPosition = _lookTransform.position +
                                         new Vector3(Random.Range(-1f, 1f) * _searchDistance, Random.Range(-1f, 1f) * _searchDistance,
                                             Random.Range(-1f, 1f) * _searchDistance);
                _lookTransform.DOMove(randomPosition, _moveDuration);
                yield return new WaitForSeconds(_waitTime);
            }

            _searchState.SwitchToPatrolState();
        }
    }
}