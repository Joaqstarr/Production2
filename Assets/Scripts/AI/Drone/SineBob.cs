using System;
using UnityEngine;

namespace AI.Drone
{
    public class SineBob : MonoBehaviour
    {
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;

        private Vector3 _startPos;
        private void Start()
        {
            _startPos = transform.localPosition;
        }

        private void Update()
        {
            transform.localPosition = _startPos + (Vector3.up * (Mathf.Sin(Time.time * _frequency) * _amplitude));
        }
    }
}