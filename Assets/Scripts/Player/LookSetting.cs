using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookSettings", menuName = "Player/LookSettings")]
public class LookSettings : ScriptableObject
{
    public Vector2 lookSensitivity = Vector2.one;
    public Vector2 minMaxVerticalLook = new Vector2(-75f, 75f);
}