using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyTools.Character.Scripts.New_Input_System
{
    public class CharacterInput : MonoBehaviour, IInteractInput
    {
        private PlayerInputActions _inputActions;
        
        public bool IsPressingInteract { get; private set; } //private set doesn't break interface contract
        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Player.Interact.performed += OnInteractButton;//add button to memory

        }

        private void OnDisable()
        {
            _inputActions.Player.Interact.performed -= OnInteractButton;
        }

        private void OnInteractButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();//float of how far button is pushed down
            IsPressingInteract = value >= 0.15;
        }

        
    }
}
