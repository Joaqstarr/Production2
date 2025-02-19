using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSettings", menuName = "Player/JumpSettings")]
public class JumpSettings : ScriptableObject
{
    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float fallMultiplier = 2f;  
    public float lowJumpMultiplier = 2f;
    public float jumpSpeedMultiplier = 0.75f;
    public float coyoteTime = 0.2f;  
    public float jumpBufferTime = 0.2f;
}