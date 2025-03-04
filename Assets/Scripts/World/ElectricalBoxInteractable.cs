using System;
using DG.Tweening;
using Player;
using Player.Animation;
using UnityEngine;

namespace World
{
    public class ElectricalBoxInteractable : PlayerInteractable
    {
        [SerializeField] private Transform _doorHinge;
        [SerializeField] private Vector3 _openDoorEulers;
        [SerializeField] private float _openTime = 0.2f;
        
        [SerializeField] private Transform _location;
        protected override void PlayerInteractedWith()
        {
            base.PlayerInteractedWith();

            
            CutsceneManager.StartCutscene(CutsceneManager.Cutscenes.Win, _location);
        }

        private void OnEnable()
        {
            PlayerCutsceneManager.OnOpenElectricalBox += OpenDoor;
        }

        private void OnDisable()
        {
            PlayerCutsceneManager.OnOpenElectricalBox -= OpenDoor;
        }

        public void OpenDoor()
        {
            _doorHinge.DOLocalRotate(_openDoorEulers, _openTime).SetEase(Ease.OutBounce);
        }

    }
    
}