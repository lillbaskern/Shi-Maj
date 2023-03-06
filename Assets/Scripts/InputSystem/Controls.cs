//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Controls.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""da14173b-db77-4fab-880c-cb952b4e4896"",
            ""actions"": [
                {
                    ""name"": ""ShootHigh"",
                    ""type"": ""Button"",
                    ""id"": ""d57f6adf-c0b6-4979-ae5a-ece4b07219f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootLow"",
                    ""type"": ""Button"",
                    ""id"": ""02805d94-d4a9-4e7c-86bf-cf21e023bb12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b36dbf90-b9a8-4e93-aa4e-aca69629e1a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8cd455bc-e8a6-4e75-a415-fbcadb574f11"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""06863998-c30d-4900-ab46-4094f5ff5bc2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""dd76c8fa-8d68-4f52-9a4f-5c04e73a104f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextChar"",
                    ""type"": ""Button"",
                    ""id"": ""b7790158-64da-44ea-abbc-aeb7ab76d1ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrevChar"",
                    ""type"": ""Button"",
                    ""id"": ""e7d97a3e-6322-4396-be21-960990a2c2f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""66387204-df9f-443b-a430-5bfadf92f6c7"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootHigh"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bc4d0b4-6e57-43a1-949c-68d118f6718f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootLow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00c2e7d5-5909-4dad-8759-6d2355f7dddb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d67cdc71-75a2-46e8-a956-1a9d6f64fd65"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""0f455039-d358-45f8-9ebd-5f436107015b"",
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
                    ""id"": ""14f6a6f2-f280-450b-a7d5-586491e0b2c4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Classic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c14a7e8e-1487-410f-8e11-17c7e335d51c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Classic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""25b4e10f-b4e5-44e6-a7e2-156bd7ef7907"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Classic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b455e5b-2fd0-4103-80aa-077de5ed9c74"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Classic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""J och L"",
                    ""id"": ""263ac3ca-eabc-42b7-aa21-37778fe71090"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ac8b04c8-d726-4a1e-b742-66b8205e5a30"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6f4232ad-a1d4-4792-b73f-9e108f878ea4"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""258e1080-4de4-4074-950c-a0f1de8ff784"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""82342a8f-2e2d-474b-8ad3-c9c73233867c"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Classic"",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""56949bea-5887-4c72-b65a-19fe8ab49242"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae2afe1e-32fa-4e64-a2e4-ecb8a2ba1064"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e613741-8810-4f0f-ad1b-ce00da76ef13"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrevChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Classic"",
            ""bindingGroup"": ""Classic"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_ShootHigh = m_Player.FindAction("ShootHigh", throwIfNotFound: true);
        m_Player_ShootLow = m_Player.FindAction("ShootLow", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Turn = m_Player.FindAction("Turn", throwIfNotFound: true);
        m_Player_Special = m_Player.FindAction("Special", throwIfNotFound: true);
        m_Player_NextChar = m_Player.FindAction("NextChar", throwIfNotFound: true);
        m_Player_PrevChar = m_Player.FindAction("PrevChar", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_ShootHigh;
    private readonly InputAction m_Player_ShootLow;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Turn;
    private readonly InputAction m_Player_Special;
    private readonly InputAction m_Player_NextChar;
    private readonly InputAction m_Player_PrevChar;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ShootHigh => m_Wrapper.m_Player_ShootHigh;
        public InputAction @ShootLow => m_Wrapper.m_Player_ShootLow;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Turn => m_Wrapper.m_Player_Turn;
        public InputAction @Special => m_Wrapper.m_Player_Special;
        public InputAction @NextChar => m_Wrapper.m_Player_NextChar;
        public InputAction @PrevChar => m_Wrapper.m_Player_PrevChar;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @ShootHigh.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootHigh;
                @ShootHigh.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootHigh;
                @ShootHigh.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootHigh;
                @ShootLow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootLow;
                @ShootLow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootLow;
                @ShootLow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootLow;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Turn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Special.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @NextChar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextChar;
                @NextChar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextChar;
                @NextChar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextChar;
                @PrevChar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevChar;
                @PrevChar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevChar;
                @PrevChar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevChar;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ShootHigh.started += instance.OnShootHigh;
                @ShootHigh.performed += instance.OnShootHigh;
                @ShootHigh.canceled += instance.OnShootHigh;
                @ShootLow.started += instance.OnShootLow;
                @ShootLow.performed += instance.OnShootLow;
                @ShootLow.canceled += instance.OnShootLow;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @NextChar.started += instance.OnNextChar;
                @NextChar.performed += instance.OnNextChar;
                @NextChar.canceled += instance.OnNextChar;
                @PrevChar.started += instance.OnPrevChar;
                @PrevChar.performed += instance.OnPrevChar;
                @PrevChar.canceled += instance.OnPrevChar;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_ClassicSchemeIndex = -1;
    public InputControlScheme ClassicScheme
    {
        get
        {
            if (m_ClassicSchemeIndex == -1) m_ClassicSchemeIndex = asset.FindControlSchemeIndex("Classic");
            return asset.controlSchemes[m_ClassicSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnShootHigh(InputAction.CallbackContext context);
        void OnShootLow(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnNextChar(InputAction.CallbackContext context);
        void OnPrevChar(InputAction.CallbackContext context);
    }
}
