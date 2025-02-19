using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GravitySettings", menuName = "Player/GravitySettings")]
public class GravitySettings : ScriptableObject
{
    public float gravity = -9.81f;
}