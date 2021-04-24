// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""2adf7152-bbc6-4aac-b687-8eb33f322af2"",
            ""actions"": [
                {
                    ""name"": ""PauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""37a049ff-ecc2-410c-8fb7-1162fc47680b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ca911f18-dd60-49a8-a21f-cb282401dc3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap"",
                    ""type"": ""Button"",
                    ""id"": ""7e650d75-698f-4b97-a7ec-269e6d04b1ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""8b74d295-0f5b-4a48-bb9d-48da694f4f7d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionOne"",
                    ""type"": ""Button"",
                    ""id"": ""3b1a955a-207b-41ed-8ca2-3ab887c187aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionTwo"",
                    ""type"": ""Button"",
                    ""id"": ""c4a663a2-1caf-43c5-9602-2f2cb5de3a02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bf9ef834-42b1-4ed5-bbc6-7b42a3ef26d7"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7a8a0f1-c9d8-416d-9f42-b10db7febb37"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e9ce69e4-ff0b-4030-b11c-c087bc4745f4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d331eb02-92cd-48b7-adba-4e757559d0e4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1ac2b803-6da3-4da8-b0db-0b85ce831d98"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7f81aa20-6aa2-4bc2-841f-314720872dcb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""79ec2498-2228-4432-a846-4c3ded4e0db0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""08b627db-dc44-4799-9b79-f5b5810a29b3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""54b452a3-efbd-4e09-ba71-e31697636a69"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""af36fa97-1d8f-4500-b6b5-2bd6f116601f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bbd69040-3b86-498f-9cd5-95a85b14b229"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""695acb7e-2fde-4658-a11c-e966fcf1e742"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f86c61f8-2f2b-4e44-8f52-994189581899"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f002af0a-4164-4972-af34-d2d9b846a7f1"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f3796df-e8b4-4299-9c8b-8ace1ec7089b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ActionOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dead758f-8498-4e23-876b-45c485566fa6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ActionTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_PauseMenu = m_Gameplay.FindAction("PauseMenu", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Swap = m_Gameplay.FindAction("Swap", throwIfNotFound: true);
        m_Gameplay_MousePosition = m_Gameplay.FindAction("MousePosition", throwIfNotFound: true);
        m_Gameplay_ActionOne = m_Gameplay.FindAction("ActionOne", throwIfNotFound: true);
        m_Gameplay_ActionTwo = m_Gameplay.FindAction("ActionTwo", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_PauseMenu;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Swap;
    private readonly InputAction m_Gameplay_MousePosition;
    private readonly InputAction m_Gameplay_ActionOne;
    private readonly InputAction m_Gameplay_ActionTwo;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseMenu => m_Wrapper.m_Gameplay_PauseMenu;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Swap => m_Wrapper.m_Gameplay_Swap;
        public InputAction @MousePosition => m_Wrapper.m_Gameplay_MousePosition;
        public InputAction @ActionOne => m_Wrapper.m_Gameplay_ActionOne;
        public InputAction @ActionTwo => m_Wrapper.m_Gameplay_ActionTwo;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @PauseMenu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseMenu;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Swap.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwap;
                @Swap.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwap;
                @Swap.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwap;
                @MousePosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @ActionOne.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionOne;
                @ActionOne.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionOne;
                @ActionOne.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionOne;
                @ActionTwo.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionTwo;
                @ActionTwo.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionTwo;
                @ActionTwo.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionTwo;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Swap.started += instance.OnSwap;
                @Swap.performed += instance.OnSwap;
                @Swap.canceled += instance.OnSwap;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @ActionOne.started += instance.OnActionOne;
                @ActionOne.performed += instance.OnActionOne;
                @ActionOne.canceled += instance.OnActionOne;
                @ActionTwo.started += instance.OnActionTwo;
                @ActionTwo.performed += instance.OnActionTwo;
                @ActionTwo.canceled += instance.OnActionTwo;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSwap(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnActionOne(InputAction.CallbackContext context);
        void OnActionTwo(InputAction.CallbackContext context);
    }
}
