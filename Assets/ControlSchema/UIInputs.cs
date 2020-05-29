// GENERATED AUTOMATICALLY FROM 'Assets/ControlSchema/UIInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UIInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIInputs"",
    ""maps"": [
        {
            ""name"": ""UIActions"",
            ""id"": ""8827be03-d357-4046-ad2e-4bf1019812ac"",
            ""actions"": [
                {
                    ""name"": ""BuildMenu"",
                    ""type"": ""Button"",
                    ""id"": ""9feeec87-8c7f-49ca-942f-236588b4cecc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BuildingMenue"",
                    ""type"": ""Button"",
                    ""id"": ""84f1ffef-dcf9-429b-97c1-7f554ff95b27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""b251547a-74d1-4174-8f47-1f45b5f1e54b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DestroyObject"",
                    ""type"": ""Button"",
                    ""id"": ""94eb2315-17d1-461c-a670-b12ea73cf5cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73ba6535-3e60-4a09-9e92-4f4d23118459"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""BuildMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e75b3efd-d5cf-4804-900e-2159c28b3b03"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""BuildingMenue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc31abbf-588e-4419-9913-c570bfbefac9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""382e83e2-749b-4b51-8d2c-44c798f5b9e1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""DestroyObject"",
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
        }
    ]
}");
        // UIActions
        m_UIActions = asset.FindActionMap("UIActions", throwIfNotFound: true);
        m_UIActions_BuildMenu = m_UIActions.FindAction("BuildMenu", throwIfNotFound: true);
        m_UIActions_BuildingMenue = m_UIActions.FindAction("BuildingMenue", throwIfNotFound: true);
        m_UIActions_CloseMenu = m_UIActions.FindAction("CloseMenu", throwIfNotFound: true);
        m_UIActions_DestroyObject = m_UIActions.FindAction("DestroyObject", throwIfNotFound: true);
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

    // UIActions
    private readonly InputActionMap m_UIActions;
    private IUIActionsActions m_UIActionsActionsCallbackInterface;
    private readonly InputAction m_UIActions_BuildMenu;
    private readonly InputAction m_UIActions_BuildingMenue;
    private readonly InputAction m_UIActions_CloseMenu;
    private readonly InputAction m_UIActions_DestroyObject;
    public struct UIActionsActions
    {
        private @UIInputs m_Wrapper;
        public UIActionsActions(@UIInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @BuildMenu => m_Wrapper.m_UIActions_BuildMenu;
        public InputAction @BuildingMenue => m_Wrapper.m_UIActions_BuildingMenue;
        public InputAction @CloseMenu => m_Wrapper.m_UIActions_CloseMenu;
        public InputAction @DestroyObject => m_Wrapper.m_UIActions_DestroyObject;
        public InputActionMap Get() { return m_Wrapper.m_UIActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActionsActions set) { return set.Get(); }
        public void SetCallbacks(IUIActionsActions instance)
        {
            if (m_Wrapper.m_UIActionsActionsCallbackInterface != null)
            {
                @BuildMenu.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnBuildMenu;
                @BuildMenu.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnBuildMenu;
                @BuildMenu.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnBuildMenu;
                @BuildingMenue.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnBuildingMenue;
                @BuildingMenue.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnBuildingMenue;
                @BuildingMenue.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnBuildingMenue;
                @CloseMenu.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnCloseMenu;
                @DestroyObject.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnDestroyObject;
                @DestroyObject.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnDestroyObject;
                @DestroyObject.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnDestroyObject;
            }
            m_Wrapper.m_UIActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BuildMenu.started += instance.OnBuildMenu;
                @BuildMenu.performed += instance.OnBuildMenu;
                @BuildMenu.canceled += instance.OnBuildMenu;
                @BuildingMenue.started += instance.OnBuildingMenue;
                @BuildingMenue.performed += instance.OnBuildingMenue;
                @BuildingMenue.canceled += instance.OnBuildingMenue;
                @CloseMenu.started += instance.OnCloseMenu;
                @CloseMenu.performed += instance.OnCloseMenu;
                @CloseMenu.canceled += instance.OnCloseMenu;
                @DestroyObject.started += instance.OnDestroyObject;
                @DestroyObject.performed += instance.OnDestroyObject;
                @DestroyObject.canceled += instance.OnDestroyObject;
            }
        }
    }
    public UIActionsActions @UIActions => new UIActionsActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    public interface IUIActionsActions
    {
        void OnBuildMenu(InputAction.CallbackContext context);
        void OnBuildingMenue(InputAction.CallbackContext context);
        void OnCloseMenu(InputAction.CallbackContext context);
        void OnDestroyObject(InputAction.CallbackContext context);
    }
}
