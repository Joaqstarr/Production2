using System;
using System.Collections;
using UnityEngine;
using World;

namespace Player.Animation
{
    public class StartCutscenePlayer : MonoBehaviour
    {
        private void Start()
        {
            CutsceneManager.StartCutscene(CutsceneManager.Cutscenes.StartGame, transform);

        }
    }
}