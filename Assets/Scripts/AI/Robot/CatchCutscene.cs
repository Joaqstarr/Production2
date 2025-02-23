using System.Collections;
using Player;
using Player.Animation;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI.Robot
{
    public class CatchCutscene : MonoBehaviour
    {
        [SerializeField] private Animator _robotAnimator;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _catchPosition;
        [SerializeField] private CutsceneCam _cutsceneCam;
        [SerializeField] private string _catchTrigger = "Catch";
        [SerializeField] private string _caughtTrigger = "Caught";

        private bool isPlayingCutscene = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isPlayingCutscene)
            {
                isPlayingCutscene = true;
                StartCoroutine(PlayCatchCutscene(other.transform));
            }
        }

        private IEnumerator PlayCatchCutscene(Transform player)
        {
            // Disable player movement and AI logic
            player.GetComponent<PlayerManager>().enabled = false;
            //GetComponent<RobotBrain>().enabled = false;

            transform.forward = (player.position - transform.position).normalized;
            
            _cutsceneCam.PlayCaughtCutscene(transform);

            // Move player to catch position
            player.position = transform.position;
            player.rotation = transform.rotation;

            // Trigger animations
            _robotAnimator.SetTrigger(_catchTrigger);
            _playerAnimator.SetTrigger(_caughtTrigger);

            // Wait for animations to finish
            float animationDuration = _robotAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationDuration);

            // Re-enable player control or trigger a game over state
            //player.GetComponent<PlayerManager>().enabled = true;
            isPlayingCutscene = false;
        }
    }
}