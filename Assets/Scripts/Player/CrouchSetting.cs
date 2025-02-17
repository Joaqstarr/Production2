using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrouchSettings", menuName = "Player/CrouchSettings")]
public class CrouchSettings : ScriptableObject
{
    [Header("Crouch Settings")]
    public float crouchSpeed = 2.5f;
    public float crouchHeight = 1f;
    public float standingHeight = 2f;
    public float crouchTransitionSpeed = 6f;
    public float cameraCrouchOffset = 0.5f;
}
