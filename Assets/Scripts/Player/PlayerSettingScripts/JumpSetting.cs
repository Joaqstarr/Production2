using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSettings", menuName = "Player/JumpSettings")]
public class JumpSettings : ScriptableObject
{
    [Header("Jump Settings")]
    public float jumpHeight = 0.5f;
    public float fallMultiplier = 2f;  
}