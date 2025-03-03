using DG.Tweening;
using UnityEngine;

namespace World
{
    public class Jostler : MonoBehaviour
    {
        [SerializeField] private float _shakeDuration = 1f;
        [SerializeField] private float _shakeStrength = 1f;
        [SerializeField] private int _shakeVibrato = 10;
        [SerializeField] private float _shakeRandomness = 90f;
        [SerializeField] private float _scaleShakeStrength = 0.1f;

        private void Start()
        {
            float randomDelay = Random.Range(0f, 1f);
            Invoke(nameof(StartJostling), randomDelay);
        }

        private void StartJostling()
        {
            transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness)
                .OnComplete(StartJostling);
            transform.DOShakeScale(_shakeDuration, _scaleShakeStrength, _shakeVibrato, _shakeRandomness);
        }
    }
}