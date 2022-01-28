//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Scripts/Controls/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerOnFoot"",
            ""id"": ""494f29fd-7048-4043-ae69-24039dd18b22"",
            ""actions"": [
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bfe85d44-9c33-4902-8e6a-8d58ac97a277"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""53b11020-c1ff-44f4-9366-a8b73e7b4004"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jumping"",
                    ""type"": ""Button"",
                    ""id"": ""d3542c75-6a7c-43ef-87de-d6ffe44ae1ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Running"",
                    ""type"": ""Button"",
                    ""id"": ""5e03560f-b6d0-4a45-b991-354602b952ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouching"",
                    ""type"": ""Button"",
                    ""id"": ""f2eaaa6b-ba69-495f-ba16-560e59da47fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interacting"",
                    ""type"": ""Button"",
                    ""id"": ""29d393d0-5f5d-4c2e-9935-5a713a2f8e7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zooming"",
                    ""type"": ""Button"",
                    ""id"": ""6168f1a5-6f9a-4995-bd93-753a06eb7c1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""26d495b8-228b-4cc7-84e5-faca06a3348d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dd31d9b-1ab9-4a03-8947-5072069eb210"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""deeb8436-54c7-4526-852d-f7f05af3946e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4b96ac30-0f1e-4063-bccf-65190fedef52"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f135c517-3331-4676-8d6c-9303a184f356"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""298f0844-a08b-49f2-8268-ce8214b227d0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3b6afd76-6b82-4ed3-8085-5ed90cbf99f1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""072e5b54-6d4f-4ad3-905a-95db821cd283"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd02308f-7fe6-4182-a74d-14227c193534"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c983e9e9-6158-4a78-8e0c-5ed79b82986b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ac58e41-5d97-4e61-b159-d07cbae100c6"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Running"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c52a5c7-50e4-4bc8-9b46-444ac2ec4ac6"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Running"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf81083f-9af9-4e4a-a3f6-9f6314613ba0"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f95eed9-250d-459f-8c6d-fd0dfc1d80dd"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdbc8a83-1534-44a0-9f97-f1f68e3e40b0"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interacting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0482da2d-1bf6-4b5a-ac1c-8af5655cd44f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interacting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d83fd63-07ca-4e39-896a-4e59d93c1653"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zooming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""698124cb-b45f-484d-8aed-62d30194f3d0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zooming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerOnFoot
        m_PlayerOnFoot = asset.FindActionMap("PlayerOnFoot", throwIfNotFound: true);
        m_PlayerOnFoot_MouseLook = m_PlayerOnFoot.FindAction("MouseLook", throwIfNotFound: true);
        m_PlayerOnFoot_Movement = m_PlayerOnFoot.FindAction("Movement", throwIfNotFound: true);
        m_PlayerOnFoot_Jumping = m_PlayerOnFoot.FindAction("Jumping", throwIfNotFound: true);
        m_PlayerOnFoot_Running = m_PlayerOnFoot.FindAction("Running", throwIfNotFound: true);
        m_PlayerOnFoot_Crouching = m_PlayerOnFoot.FindAction("Crouching", throwIfNotFound: true);
        m_PlayerOnFoot_Interacting = m_PlayerOnFoot.FindAction("Interacting", throwIfNotFound: true);
        m_PlayerOnFoot_Zooming = m_PlayerOnFoot.FindAction("Zooming", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerOnFoot
    private readonly InputActionMap m_PlayerOnFoot;
    private IPlayerOnFootActions m_PlayerOnFootActionsCallbackInterface;
    private readonly InputAction m_PlayerOnFoot_MouseLook;
    private readonly InputAction m_PlayerOnFoot_Movement;
    private readonly InputAction m_PlayerOnFoot_Jumping;
    private readonly InputAction m_PlayerOnFoot_Running;
    private readonly InputAction m_PlayerOnFoot_Crouching;
    private readonly InputAction m_PlayerOnFoot_Interacting;
    private readonly InputAction m_PlayerOnFoot_Zooming;
    public struct PlayerOnFootActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerOnFootActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLook => m_Wrapper.m_PlayerOnFoot_MouseLook;
        public InputAction @Movement => m_Wrapper.m_PlayerOnFoot_Movement;
        public InputAction @Jumping => m_Wrapper.m_PlayerOnFoot_Jumping;
        public InputAction @Running => m_Wrapper.m_PlayerOnFoot_Running;
        public InputAction @Crouching => m_Wrapper.m_PlayerOnFoot_Crouching;
        public InputAction @Interacting => m_Wrapper.m_PlayerOnFoot_Interacting;
        public InputAction @Zooming => m_Wrapper.m_PlayerOnFoot_Zooming;
        public InputActionMap Get() { return m_Wrapper.m_PlayerOnFoot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerOnFootActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerOnFootActions instance)
        {
            if (m_Wrapper.m_PlayerOnFootActionsCallbackInterface != null)
            {
                @MouseLook.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnMouseLook;
                @Movement.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnMovement;
                @Jumping.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnJumping;
                @Jumping.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnJumping;
                @Jumping.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnJumping;
                @Running.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnRunning;
                @Running.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnRunning;
                @Running.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnRunning;
                @Crouching.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnCrouching;
                @Crouching.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnCrouching;
                @Crouching.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnCrouching;
                @Interacting.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnInteracting;
                @Interacting.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnInteracting;
                @Interacting.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnInteracting;
                @Zooming.started -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnZooming;
                @Zooming.performed -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnZooming;
                @Zooming.canceled -= m_Wrapper.m_PlayerOnFootActionsCallbackInterface.OnZooming;
            }
            m_Wrapper.m_PlayerOnFootActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jumping.started += instance.OnJumping;
                @Jumping.performed += instance.OnJumping;
                @Jumping.canceled += instance.OnJumping;
                @Running.started += instance.OnRunning;
                @Running.performed += instance.OnRunning;
                @Running.canceled += instance.OnRunning;
                @Crouching.started += instance.OnCrouching;
                @Crouching.performed += instance.OnCrouching;
                @Crouching.canceled += instance.OnCrouching;
                @Interacting.started += instance.OnInteracting;
                @Interacting.performed += instance.OnInteracting;
                @Interacting.canceled += instance.OnInteracting;
                @Zooming.started += instance.OnZooming;
                @Zooming.performed += instance.OnZooming;
                @Zooming.canceled += instance.OnZooming;
            }
        }
    }
    public PlayerOnFootActions @PlayerOnFoot => new PlayerOnFootActions(this);
    public interface IPlayerOnFootActions
    {
        void OnMouseLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJumping(InputAction.CallbackContext context);
        void OnRunning(InputAction.CallbackContext context);
        void OnCrouching(InputAction.CallbackContext context);
        void OnInteracting(InputAction.CallbackContext context);
        void OnZooming(InputAction.CallbackContext context);
    }
}
