using System;
using System.Collections;
using System.Collections.Generic;
using AI.Robot.Animation;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DisableIKOnCaught : MonoBehaviour
{
    private Rig _rig;

    private void Awake()
    {
        _rig = GetComponent<Rig>();
    }

    private void OnEnable()
    {
        CatchCutscene.OnCaughtCutscneTriggered += OnCaughtCutsceneTriggered;
    }

    private void OnDisable()
    {
        CatchCutscene.OnCaughtCutscneTriggered -= OnCaughtCutsceneTriggered;
    }

    private void OnCaughtCutsceneTriggered(Transform player)
    {
        _rig.weight = 0;
    }
}
