// GENERATED AUTOMATICALLY FROM 'Assets/ControlSchema/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""MoveAction"",
            ""id"": ""320a8397-a6fb-4330-833c-7ace546c59be"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bdfb9c27-533c-4ce2-8814-e8c2df53b740"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""1e105bed-9a3f-40ae-b86c-f27bdcb43ba9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""49f790ff-a2b5-40f0-9967-c7ff57c2fc41"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""8c39126c-1c9e-4955-972a-fc59404bd4c6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""62d7ca71-8e2e-4b38-bb25-05a8d35fec50"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6033e145-f591-47ea-942b-959055880701"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""750e51a9-0dc0-4ebe-a3b8-538b7923d9ba"",
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
                    ""id"": ""a6ad90c5-8ca2-42a2-872f-c577555c176e"",
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
                    ""id"": ""415b533c-e67c-4483-b09e-14377498ba09"",
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
                    ""id"": ""eb4ff7fb-8843-4e2e-b229-faf7b30247c1"",
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
                    ""id"": ""a39daa15-7f5b-4db2-bec2-398f83da3421"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""99e6899b-7eff-4e99-8b56-7df8a42a68c2"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18a4ded5-ccc5-420a-ac08-0e4e2b6a59ea"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39220f26-5643-47c8-8f9c-79ad2901fde0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09562e36-d705-46a0-bff9-175181af45d1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beebc9dc-9e27-4b0f-a057-1dc3334d9b2e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3d811a2-5c20-40d3-bed7-d5c1a4c6e63f"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66e1f2db-4f60-4ebf-9d6e-b61900fb2eb1"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""696f108e-cbd6-4824-b212-d694225aca37"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
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
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MoveAction
        m_MoveAction = asset.FindActionMap("MoveAction", throwIfNotFound: true);
        m_MoveAction_Move = m_MoveAction.FindAction("Move", throwIfNotFound: true);
        m_MoveAction_Run = m_MoveAction.FindAction("Run", throwIfNotFound: true);
        m_MoveAction_Jump = m_MoveAction.FindAction("Jump", throwIfNotFound: true);
        m_MoveAction_Crouch = m_MoveAction.FindAction("Crouch", throwIfNotFound: true);
        m_MoveAction_Look = m_MoveAction.FindAction("Look", throwIfNotFound: true);
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

    // MoveAction
    private readonly InputActionMap m_MoveAction;
    private IMoveActionActions m_MoveActionActionsCallbackInterface;
    private readonly InputAction m_MoveAction_Move;
    private readonly InputAction m_MoveAction_Run;
    private readonly InputAction m_MoveAction_Jump;
    private readonly InputAction m_MoveAction_Crouch;
    private readonly InputAction m_MoveAction_Look;
    public struct MoveActionActions
    {
        private @PlayerActions m_Wrapper;
        public MoveActionActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MoveAction_Move;
        public InputAction @Run => m_Wrapper.m_MoveAction_Run;
        public InputAction @Jump => m_Wrapper.m_MoveAction_Jump;
        public InputAction @Crouch => m_Wrapper.m_MoveAction_Crouch;
        public InputAction @Look => m_Wrapper.m_MoveAction_Look;
        public InputActionMap Get() { return m_Wrapper.m_MoveAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActionActions set) { return set.Get(); }
        public void SetCallbacks(IMoveActionActions instance)
        {
            if (m_Wrapper.m_MoveActionActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnCrouch;
                @Look.started -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MoveActionActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_MoveActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public MoveActionActions @MoveAction => new MoveActionActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IMoveActionActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
