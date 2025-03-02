using System.Collections;
using Player;
using Player.Animation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using World;

namespace AI.Robot.Animation
{
    public class CatchCutscene : MonoBehaviour
    {
        public delegate void CutsceneTriggerDelegate(Transform player);
        public static CutsceneTriggerDelegate OnCaughtCutscneTriggered;
        
        [SerializeField] private Animator _robotAnimator;
        [SerializeField] private Transform _catchPosition;
        private static readonly int CatchTrigger = Animator.StringToHash("Catch");
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
            
            
            transform.forward = (player.transform.position - transform.position).normalized;

            OnCaughtCutscneTriggered?.Invoke(transform);
            CutsceneManager.StartCutscene(CutsceneManager.Cutscenes.Caught, transform);


            


            // Trigger animations
            _robotAnimator.SetTrigger(CatchTrigger);

            // Wait for animations to finish
            float animationDuration = _robotAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationDuration);

            // Re-enable player control or trigger a game over state
            //player.GetComponent<PlayerManager>().enabled = true;
            isPlayingCutscene = false;
        }

        public void ReloadScene()
        {
            CutsceneManager.EndCutscene(CutsceneManager.Cutscenes.Caught);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}