using System;
using UnityEngine;
using World;

namespace AI.Robot.Animation
{
    public class WinAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _animator.gameObject.SetActive(false);
            
        }

        private void OnEnable()
        {
            CutsceneManager.OnPlayCutscene += OnPlayCutscene;

        }

        private void OnDisable()
        {
            CutsceneManager.OnPlayCutscene -= OnPlayCutscene;

        }

        private void OnPlayCutscene(CutsceneManager.Cutscenes cutscene, Transform location)
        {
            if (cutscene == CutsceneManager.Cutscenes.Win)
            {
                _animator.gameObject.SetActive(true);
                _animator.SetTrigger("Win");
            }
        }
    }
}