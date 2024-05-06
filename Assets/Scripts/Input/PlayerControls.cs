//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Action Map"",
            ""id"": ""c90ee69f-e076-4838-baa8-71bfa4536e74"",
            ""actions"": [
                {
                    ""name"": ""Open Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""0ca1c273-fc31-4b10-8458-d2bb6ad202bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""03c898f8-a190-4842-996e-6cb061ea7433"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d3f23f85-1176-4059-beac-ef6ea01b5d79"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""04059539-e0f3-4ef6-87dd-17be95967262"",
                    ""path"": ""<Keyboard>/#(I)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9527e99-0a41-4aa9-aca9-19a42481c11e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""d9db27ef-0289-4f0e-b1f1-31d37f884663"",
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
                    ""id"": ""59e6ee7a-1f94-4dfc-9d1d-dd6a106c6877"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ebeedfd2-cd9a-4483-b21f-042c22fcdddd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""565cc556-2186-4cae-ad5e-af886aaf3a81"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18effd6a-8e69-4ef4-b4dc-ac43d6673bb7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""ObjectClick Map"",
            ""id"": ""8ff5693c-36d6-4cbb-9ad7-0b925c2e64e2"",
            ""actions"": [
                {
                    ""name"": ""Object Click"",
                    ""type"": ""Button"",
                    ""id"": ""0dce766f-8e6c-44b1-9df1-35759dd0cffd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""36a78761-4279-4486-8c63-4de22fb61c42"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Object Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""EnemyClick Map"",
            ""id"": ""84a4960f-ff0e-4f94-8922-301553cfcddf"",
            ""actions"": [
                {
                    ""name"": ""Enemy Click"",
                    ""type"": ""Button"",
                    ""id"": ""0218e505-4489-4691-8e84-9dfda91e7a7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""885d7099-bb90-4d92-92bb-d8109228cd19"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enemy Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NPC Click Map"",
            ""id"": ""bfc35c1c-6fb1-4ff5-8ca3-56b8e378775c"",
            ""actions"": [
                {
                    ""name"": ""NPC Click"",
                    ""type"": ""Button"",
                    ""id"": ""3ad6c01c-e434-4fe6-9d7a-205bf6d86c04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85a37979-0e85-4446-8305-b5403e4e67f9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NPC Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Action Map
        m_PlayerActionMap = asset.FindActionMap("Player Action Map", throwIfNotFound: true);
        m_PlayerActionMap_OpenInventory = m_PlayerActionMap.FindAction("Open Inventory", throwIfNotFound: true);
        m_PlayerActionMap_Pause = m_PlayerActionMap.FindAction("Pause", throwIfNotFound: true);
        m_PlayerActionMap_Move = m_PlayerActionMap.FindAction("Move", throwIfNotFound: true);
        // ObjectClick Map
        m_ObjectClickMap = asset.FindActionMap("ObjectClick Map", throwIfNotFound: true);
        m_ObjectClickMap_ObjectClick = m_ObjectClickMap.FindAction("Object Click", throwIfNotFound: true);
        // EnemyClick Map
        m_EnemyClickMap = asset.FindActionMap("EnemyClick Map", throwIfNotFound: true);
        m_EnemyClickMap_EnemyClick = m_EnemyClickMap.FindAction("Enemy Click", throwIfNotFound: true);
        // NPC Click Map
        m_NPCClickMap = asset.FindActionMap("NPC Click Map", throwIfNotFound: true);
        m_NPCClickMap_NPCClick = m_NPCClickMap.FindAction("NPC Click", throwIfNotFound: true);
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

    // Player Action Map
    private readonly InputActionMap m_PlayerActionMap;
    private List<IPlayerActionMapActions> m_PlayerActionMapActionsCallbackInterfaces = new List<IPlayerActionMapActions>();
    private readonly InputAction m_PlayerActionMap_OpenInventory;
    private readonly InputAction m_PlayerActionMap_Pause;
    private readonly InputAction m_PlayerActionMap_Move;
    public struct PlayerActionMapActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenInventory => m_Wrapper.m_PlayerActionMap_OpenInventory;
        public InputAction @Pause => m_Wrapper.m_PlayerActionMap_Pause;
        public InputAction @Move => m_Wrapper.m_PlayerActionMap_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Add(instance);
            @OpenInventory.started += instance.OnOpenInventory;
            @OpenInventory.performed += instance.OnOpenInventory;
            @OpenInventory.canceled += instance.OnOpenInventory;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IPlayerActionMapActions instance)
        {
            @OpenInventory.started -= instance.OnOpenInventory;
            @OpenInventory.performed -= instance.OnOpenInventory;
            @OpenInventory.canceled -= instance.OnOpenInventory;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IPlayerActionMapActions instance)
        {
            if (m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActionMapActions @PlayerActionMap => new PlayerActionMapActions(this);

    // ObjectClick Map
    private readonly InputActionMap m_ObjectClickMap;
    private List<IObjectClickMapActions> m_ObjectClickMapActionsCallbackInterfaces = new List<IObjectClickMapActions>();
    private readonly InputAction m_ObjectClickMap_ObjectClick;
    public struct ObjectClickMapActions
    {
        private @PlayerControls m_Wrapper;
        public ObjectClickMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ObjectClick => m_Wrapper.m_ObjectClickMap_ObjectClick;
        public InputActionMap Get() { return m_Wrapper.m_ObjectClickMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ObjectClickMapActions set) { return set.Get(); }
        public void AddCallbacks(IObjectClickMapActions instance)
        {
            if (instance == null || m_Wrapper.m_ObjectClickMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ObjectClickMapActionsCallbackInterfaces.Add(instance);
            @ObjectClick.started += instance.OnObjectClick;
            @ObjectClick.performed += instance.OnObjectClick;
            @ObjectClick.canceled += instance.OnObjectClick;
        }

        private void UnregisterCallbacks(IObjectClickMapActions instance)
        {
            @ObjectClick.started -= instance.OnObjectClick;
            @ObjectClick.performed -= instance.OnObjectClick;
            @ObjectClick.canceled -= instance.OnObjectClick;
        }

        public void RemoveCallbacks(IObjectClickMapActions instance)
        {
            if (m_Wrapper.m_ObjectClickMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IObjectClickMapActions instance)
        {
            foreach (var item in m_Wrapper.m_ObjectClickMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ObjectClickMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ObjectClickMapActions @ObjectClickMap => new ObjectClickMapActions(this);

    // EnemyClick Map
    private readonly InputActionMap m_EnemyClickMap;
    private List<IEnemyClickMapActions> m_EnemyClickMapActionsCallbackInterfaces = new List<IEnemyClickMapActions>();
    private readonly InputAction m_EnemyClickMap_EnemyClick;
    public struct EnemyClickMapActions
    {
        private @PlayerControls m_Wrapper;
        public EnemyClickMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @EnemyClick => m_Wrapper.m_EnemyClickMap_EnemyClick;
        public InputActionMap Get() { return m_Wrapper.m_EnemyClickMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EnemyClickMapActions set) { return set.Get(); }
        public void AddCallbacks(IEnemyClickMapActions instance)
        {
            if (instance == null || m_Wrapper.m_EnemyClickMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_EnemyClickMapActionsCallbackInterfaces.Add(instance);
            @EnemyClick.started += instance.OnEnemyClick;
            @EnemyClick.performed += instance.OnEnemyClick;
            @EnemyClick.canceled += instance.OnEnemyClick;
        }

        private void UnregisterCallbacks(IEnemyClickMapActions instance)
        {
            @EnemyClick.started -= instance.OnEnemyClick;
            @EnemyClick.performed -= instance.OnEnemyClick;
            @EnemyClick.canceled -= instance.OnEnemyClick;
        }

        public void RemoveCallbacks(IEnemyClickMapActions instance)
        {
            if (m_Wrapper.m_EnemyClickMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IEnemyClickMapActions instance)
        {
            foreach (var item in m_Wrapper.m_EnemyClickMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_EnemyClickMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public EnemyClickMapActions @EnemyClickMap => new EnemyClickMapActions(this);

    // NPC Click Map
    private readonly InputActionMap m_NPCClickMap;
    private List<INPCClickMapActions> m_NPCClickMapActionsCallbackInterfaces = new List<INPCClickMapActions>();
    private readonly InputAction m_NPCClickMap_NPCClick;
    public struct NPCClickMapActions
    {
        private @PlayerControls m_Wrapper;
        public NPCClickMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @NPCClick => m_Wrapper.m_NPCClickMap_NPCClick;
        public InputActionMap Get() { return m_Wrapper.m_NPCClickMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NPCClickMapActions set) { return set.Get(); }
        public void AddCallbacks(INPCClickMapActions instance)
        {
            if (instance == null || m_Wrapper.m_NPCClickMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_NPCClickMapActionsCallbackInterfaces.Add(instance);
            @NPCClick.started += instance.OnNPCClick;
            @NPCClick.performed += instance.OnNPCClick;
            @NPCClick.canceled += instance.OnNPCClick;
        }

        private void UnregisterCallbacks(INPCClickMapActions instance)
        {
            @NPCClick.started -= instance.OnNPCClick;
            @NPCClick.performed -= instance.OnNPCClick;
            @NPCClick.canceled -= instance.OnNPCClick;
        }

        public void RemoveCallbacks(INPCClickMapActions instance)
        {
            if (m_Wrapper.m_NPCClickMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(INPCClickMapActions instance)
        {
            foreach (var item in m_Wrapper.m_NPCClickMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_NPCClickMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public NPCClickMapActions @NPCClickMap => new NPCClickMapActions(this);
    public interface IPlayerActionMapActions
    {
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IObjectClickMapActions
    {
        void OnObjectClick(InputAction.CallbackContext context);
    }
    public interface IEnemyClickMapActions
    {
        void OnEnemyClick(InputAction.CallbackContext context);
    }
    public interface INPCClickMapActions
    {
        void OnNPCClick(InputAction.CallbackContext context);
    }
}