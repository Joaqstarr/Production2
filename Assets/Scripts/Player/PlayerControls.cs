using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        public Vector2 MovementInput { get; private set; }
        public Vector2 LookInput { get; private set; }
    
        void OnLook(InputValue input)
        {
            LookInput = input.Get<Vector2>();
        }

        void OnMove(InputValue input)
        {
            MovementInput = input.Get<Vector2>();
        }
    }
}
