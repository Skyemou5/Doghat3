﻿using System;
using MyTools.Character.Scripts.New_Input_System.Monobehaviors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyTools.Character.Scripts.New_Input_System
{
    public class CharacterInput : MonoBehaviour, IInteractInput,IMoveInput,IRotationInput,ISkillInput
    {
        public Command interactInput;
        public Command movementInput;
        public Command analogRotationInput;
        public Command mouseRotationInput;
        public Command skillInput;

        private PlayerInputActions _inputActions;
        
        // Properties
        public Vector3 MoveDirection { get; private set; }
        public Vector3 RotationDirection { get; set; }
        public bool IsUsingSkill { get; private set; }
        public bool IsPressingInteract { get; private set; } //private set doesn't break interface contract
        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            if (movementInput)
            {
                _inputActions.Player.Movement.performed += OnMoveInput;
            }
            _inputActions.Player.Interact.performed += OnInteractButton;//add button to memory
            if (analogRotationInput)
            {
                _inputActions.Player.AnalogAim.performed += OnAnalogAimInput;
            }

            if (mouseRotationInput)
            {
                _inputActions.Player.MouseAim.performed += OnMouseAimInput;
            }

            if (skillInput)
            {
                _inputActions.Player.Skill.performed += OnSkillButton;
            }
            
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            
            MoveDirection = new Vector3(value.x,0,value.y);

            if (movementInput != null)
            {
                movementInput.Execute();
            }
        }
        
        private void OnAnalogAimInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            
            RotationDirection = new Vector3(value.x,0,value.y);

            if (analogRotationInput != null)
            {
                analogRotationInput.Execute();
            }
        }

        private void OnMouseAimInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            
            RotationDirection = new Vector3(value.x,0,value.y);

            if (mouseRotationInput != null)
            {
                mouseRotationInput.Execute();
            }
        }
        private void OnInteractButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();//float of how far button is pushed down
            IsPressingInteract = value >= 0.15f;
            if (interactInput != null && IsPressingInteract)
            {
                interactInput.Execute();
            }
        }

        private void OnSkillButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            IsUsingSkill = value >= 0.15;
            if (skillInput != null && IsUsingSkill)
            {
                skillInput.Execute();
            }
        }
        private void OnDisable()
        {
            _inputActions.Player.Interact.performed -= OnInteractButton;
            _inputActions.Disable();
        }




    }
}
