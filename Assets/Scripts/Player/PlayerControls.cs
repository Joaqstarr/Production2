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
       
        void OnLook(InputValue input)
        {
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
    }
}
