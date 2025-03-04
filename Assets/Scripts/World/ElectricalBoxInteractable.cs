using Player;
using UnityEngine;

namespace World
{
    public class ElectricalBoxInteractable : PlayerInteractable
    {
        [SerializeField] private Transform _location;
        protected override void PlayerInteractedWith()
        {
            base.PlayerInteractedWith();

            
            CutsceneManager.StartCutscene(CutsceneManager.Cutscenes.Win, _location);
        }
    }
}