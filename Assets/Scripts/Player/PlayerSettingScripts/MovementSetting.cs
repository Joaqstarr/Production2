using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Player/MovementSettings")]
public class MovementSettings : ScriptableObject
{
    public float walkSpeed = 5f;
}