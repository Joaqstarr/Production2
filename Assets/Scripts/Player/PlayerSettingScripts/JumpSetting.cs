using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSettings", menuName = "Player/JumpSettings")]
public class JumpSettings : ScriptableObject
{
    [Header("Jump Settings")]
    public float jumpHeight = 1f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
}