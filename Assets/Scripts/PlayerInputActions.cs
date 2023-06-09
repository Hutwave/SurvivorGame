//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4cb44c80-33d1-4198-b8a1-b9378f09872e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c579abe7-4f39-4a7b-ae7d-ce47ecc53a06"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DashHold"",
                    ""type"": ""Button"",
                    ""id"": ""c8f6c5bf-3f75-46e5-bb9a-7fd9e7b0c7bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DashPress"",
                    ""type"": ""Button"",
                    ""id"": ""ad5cfede-3ccb-4e19-9fb9-7924faf0d89b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill F"",
                    ""type"": ""Button"",
                    ""id"": ""0f040d79-3452-4266-b82c-12fb75cffbc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill G"",
                    ""type"": ""Button"",
                    ""id"": ""43fee716-605a-43df-b34e-bdaba64bb1dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill E"",
                    ""type"": ""Button"",
                    ""id"": ""a6a951bc-5801-482e-b1f7-81c6ab78afeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill R"",
                    ""type"": ""Button"",
                    ""id"": ""1c0dafae-f252-48af-850b-86bd63735a5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e217c289-8390-495e-b114-161dd9f9197d"",
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
                    ""id"": ""305e4575-4dcd-422e-9e39-a13f4e21f9ec"",
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
                    ""id"": ""472628d8-a474-431f-a4c8-376c781d2d76"",
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
                    ""id"": ""11cd4f33-9c2e-42af-9b8e-97ccbbe54bdc"",
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
                    ""id"": ""a90fb2e1-d653-4ca0-b818-d22cba429fa2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""723a110f-625e-4b74-abb4-7e491db9bc92"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c5a3cbb-69c1-4758-bb57-a2fe384733c5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e3ea955-92ea-4883-8a56-8be0f47cec65"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab247282-8af6-4a12-80b8-6125e424bd1d"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill G"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0eb8bde-8b27-46c4-8214-219779f2390d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ba4ceb-dfdc-4fca-88db-72845820cf29"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_DashHold = m_Player.FindAction("DashHold", throwIfNotFound: true);
        m_Player_DashPress = m_Player.FindAction("DashPress", throwIfNotFound: true);
        m_Player_SkillF = m_Player.FindAction("Skill F", throwIfNotFound: true);
        m_Player_SkillG = m_Player.FindAction("Skill G", throwIfNotFound: true);
        m_Player_SkillE = m_Player.FindAction("Skill E", throwIfNotFound: true);
        m_Player_SkillR = m_Player.FindAction("Skill R", throwIfNotFound: true);
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
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_DashHold;
    private readonly InputAction m_Player_DashPress;
    private readonly InputAction m_Player_SkillF;
    private readonly InputAction m_Player_SkillG;
    private readonly InputAction m_Player_SkillE;
    private readonly InputAction m_Player_SkillR;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @DashHold => m_Wrapper.m_Player_DashHold;
        public InputAction @DashPress => m_Wrapper.m_Player_DashPress;
        public InputAction @SkillF => m_Wrapper.m_Player_SkillF;
        public InputAction @SkillG => m_Wrapper.m_Player_SkillG;
        public InputAction @SkillE => m_Wrapper.m_Player_SkillE;
        public InputAction @SkillR => m_Wrapper.m_Player_SkillR;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @DashHold.started += instance.OnDashHold;
            @DashHold.performed += instance.OnDashHold;
            @DashHold.canceled += instance.OnDashHold;
            @DashPress.started += instance.OnDashPress;
            @DashPress.performed += instance.OnDashPress;
            @DashPress.canceled += instance.OnDashPress;
            @SkillF.started += instance.OnSkillF;
            @SkillF.performed += instance.OnSkillF;
            @SkillF.canceled += instance.OnSkillF;
            @SkillG.started += instance.OnSkillG;
            @SkillG.performed += instance.OnSkillG;
            @SkillG.canceled += instance.OnSkillG;
            @SkillE.started += instance.OnSkillE;
            @SkillE.performed += instance.OnSkillE;
            @SkillE.canceled += instance.OnSkillE;
            @SkillR.started += instance.OnSkillR;
            @SkillR.performed += instance.OnSkillR;
            @SkillR.canceled += instance.OnSkillR;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @DashHold.started -= instance.OnDashHold;
            @DashHold.performed -= instance.OnDashHold;
            @DashHold.canceled -= instance.OnDashHold;
            @DashPress.started -= instance.OnDashPress;
            @DashPress.performed -= instance.OnDashPress;
            @DashPress.canceled -= instance.OnDashPress;
            @SkillF.started -= instance.OnSkillF;
            @SkillF.performed -= instance.OnSkillF;
            @SkillF.canceled -= instance.OnSkillF;
            @SkillG.started -= instance.OnSkillG;
            @SkillG.performed -= instance.OnSkillG;
            @SkillG.canceled -= instance.OnSkillG;
            @SkillE.started -= instance.OnSkillE;
            @SkillE.performed -= instance.OnSkillE;
            @SkillE.canceled -= instance.OnSkillE;
            @SkillR.started -= instance.OnSkillR;
            @SkillR.performed -= instance.OnSkillR;
            @SkillR.canceled -= instance.OnSkillR;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDashHold(InputAction.CallbackContext context);
        void OnDashPress(InputAction.CallbackContext context);
        void OnSkillF(InputAction.CallbackContext context);
        void OnSkillG(InputAction.CallbackContext context);
        void OnSkillE(InputAction.CallbackContext context);
        void OnSkillR(InputAction.CallbackContext context);
    }
}
