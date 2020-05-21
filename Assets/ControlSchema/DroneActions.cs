// GENERATED AUTOMATICALLY FROM 'Assets/ControlSchema/DroneActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DroneActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DroneActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DroneActions"",
    ""maps"": [
        {
            ""name"": ""DroneMap"",
            ""id"": ""1a3ba2ab-5bce-465b-a9f5-27c32edcde0d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""7723cfaa-bb15-4819-9ab7-9c78c7db86d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Altitude"",
                    ""type"": ""Button"",
                    ""id"": ""d83baa5e-7361-48b5-a71a-53e36e1feffa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Speed"",
                    ""type"": ""Button"",
                    ""id"": ""e2c0769b-0368-4d59-8e34-b2dadbbb9934"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""edfa677e-7147-4604-89a6-548cf84948d0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""dadb700b-2c7d-4a7b-beba-bc53bc4592e0"",
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
                    ""id"": ""1a125044-fa76-4e7b-9a68-71a57197ff65"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8b5923dc-10ca-4422-b191-d266c0fb9615"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""994e0d37-5ae3-4750-98d1-9741f7692870"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f3c7e0a7-28f4-4705-893a-709560e17611"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Cont"",
                    ""id"": ""e050ee5a-cfe3-481f-96fa-aed0f5aaa67c"",
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
                    ""id"": ""445f8638-0ae9-4f14-af8f-164d487ee361"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2776d477-16cf-4948-be5c-5430f3c57c43"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3b355d39-2843-4d6d-bd73-53afc8745c13"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e6a3695d-948c-46ef-bd60-631acbcba129"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""dc863301-e01a-475b-ba62-f8145a442bf6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Altitude"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""75fb045e-6045-4a27-afc7-f1becff1097a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Altitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""019a405c-6d43-4d86-93de-8f15b1d9eda7"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Altitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Contoller"",
                    ""id"": ""8256ae1a-382d-402c-a22a-a49c41f75581"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Altitude"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f38f681f-f6cb-4698-9629-8801155c84b6"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Altitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""06588181-ef4b-4563-a750-8cb5f815b992"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Altitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""b6802391-ab4e-4787-93fa-a87bcf2d50f5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Speed"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c2dcc0e5-ca59-444d-a64d-52ec28860293"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Speed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""98ee98db-a09b-4f91-ace3-18e176837af9"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Speed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bd922b29-7517-4aa0-b0eb-e09cfd62c500"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
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
        // DroneMap
        m_DroneMap = asset.FindActionMap("DroneMap", throwIfNotFound: true);
        m_DroneMap_Move = m_DroneMap.FindAction("Move", throwIfNotFound: true);
        m_DroneMap_Altitude = m_DroneMap.FindAction("Altitude", throwIfNotFound: true);
        m_DroneMap_Speed = m_DroneMap.FindAction("Speed", throwIfNotFound: true);
        m_DroneMap_Look = m_DroneMap.FindAction("Look", throwIfNotFound: true);
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

    // DroneMap
    private readonly InputActionMap m_DroneMap;
    private IDroneMapActions m_DroneMapActionsCallbackInterface;
    private readonly InputAction m_DroneMap_Move;
    private readonly InputAction m_DroneMap_Altitude;
    private readonly InputAction m_DroneMap_Speed;
    private readonly InputAction m_DroneMap_Look;
    public struct DroneMapActions
    {
        private @DroneActions m_Wrapper;
        public DroneMapActions(@DroneActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_DroneMap_Move;
        public InputAction @Altitude => m_Wrapper.m_DroneMap_Altitude;
        public InputAction @Speed => m_Wrapper.m_DroneMap_Speed;
        public InputAction @Look => m_Wrapper.m_DroneMap_Look;
        public InputActionMap Get() { return m_Wrapper.m_DroneMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DroneMapActions set) { return set.Get(); }
        public void SetCallbacks(IDroneMapActions instance)
        {
            if (m_Wrapper.m_DroneMapActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnMove;
                @Altitude.started -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnAltitude;
                @Altitude.performed -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnAltitude;
                @Altitude.canceled -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnAltitude;
                @Speed.started -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnSpeed;
                @Speed.performed -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnSpeed;
                @Speed.canceled -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnSpeed;
                @Look.started -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_DroneMapActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_DroneMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Altitude.started += instance.OnAltitude;
                @Altitude.performed += instance.OnAltitude;
                @Altitude.canceled += instance.OnAltitude;
                @Speed.started += instance.OnSpeed;
                @Speed.performed += instance.OnSpeed;
                @Speed.canceled += instance.OnSpeed;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public DroneMapActions @DroneMap => new DroneMapActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IDroneMapActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAltitude(InputAction.CallbackContext context);
        void OnSpeed(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
