// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PI : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PI()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""CharacterMovement"",
            ""id"": ""a4bbfdcc-66dc-4bc9-bffc-14d6d52d867f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b6e67c08-5a7e-4892-89ea-c3b96f17b3ee"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fee48859-8288-46eb-bbba-25aef1141850"",
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
                    ""id"": ""87565382-4de3-4f5f-a7bb-7fd19f4ddc76"",
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
                    ""id"": ""1551bc14-95f1-4710-8381-f852e1c92bf1"",
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
                    ""id"": ""0fc2b20c-e4b8-47a4-b9c2-e8449155df4c"",
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
                    ""id"": ""7efb26d5-ace5-4694-8e4c-be891d62ca74"",
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
                    ""id"": ""c368f64d-c45e-4351-92ff-238ef5d359f6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterMovement
        m_CharacterMovement = asset.FindActionMap("CharacterMovement", throwIfNotFound: true);
        m_CharacterMovement_Move = m_CharacterMovement.FindAction("Move", throwIfNotFound: true);
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

    // CharacterMovement
    private readonly InputActionMap m_CharacterMovement;
    private ICharacterMovementActions m_CharacterMovementActionsCallbackInterface;
    private readonly InputAction m_CharacterMovement_Move;
    public struct CharacterMovementActions
    {
        private @PI m_Wrapper;
        public CharacterMovementActions(@PI wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterMovement_Move;
        public InputActionMap Get() { return m_Wrapper.m_CharacterMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterMovementActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterMovementActions instance)
        {
            if (m_Wrapper.m_CharacterMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_CharacterMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public CharacterMovementActions @CharacterMovement => new CharacterMovementActions(this);
    public interface ICharacterMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
