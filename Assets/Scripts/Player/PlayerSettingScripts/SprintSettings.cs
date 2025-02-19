using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SprintSettings", menuName = "Player/SprintSettings")]
public class SprintSettings : ScriptableObject
{
    [Header("Sprinting Settings")]
    public float sprintSpeed = 8f;

    [Header("Camera Zoom")]
    public float sprintFOV = 70f;
    public float normalFOV = 60f;
    public float zoomSpeed = 10f;
}
