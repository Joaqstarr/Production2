using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        public delegate void ButtonDelegate();
        
        public Vector2 MovementInput { get; private set; }
        public Vector2 LookInput { get; private set; }
    
        public bool IsLaserOn { get; private set; }
        public ButtonDelegate OnLaserPressed;
        public ButtonDelegate OnLaserUnpressed;

        public bool IsCrouching { get; private set; } = false;
        public ButtonDelegate OnCrouchPressed;

        public ButtonDelegate InteractPressed;
        void OnLook(InputValue input)
        {
            if (Time.timeScale == 0)
            {
                LookInput = Vector2.zero;
                return;
            }
            LookInput = input.Get<Vector2>();
        }

        void OnMove(InputValue input)
        {
            MovementInput = input.Get<Vector2>();
        }

        void OnLaser(InputValue input)
        {
            bool newValue =  input.Get<float>() > 0;

            if (IsLaserOn != newValue)
            {
                if (newValue)
                {
                    OnLaserPressed?.Invoke();
                }
                else
                {
                    OnLaserUnpressed?.Invoke();
                }
            }

            IsLaserOn = newValue;
        }
        
        void OnInteract()
        {
            InteractPressed?.Invoke();
        }

        void OnCrouch(InputValue input)
        {
            bool isCrouching = IsCrouching;
            
            IsCrouching = input.isPressed;
            
            if (input.isPressed != isCrouching)
            {
                OnCrouchPressed?.Invoke();
            }

        }
    }
}
