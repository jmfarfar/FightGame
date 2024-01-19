using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerInputHandler : MonoBehaviour
{
    //private PlayerInput playerInput;
    private Mover mover;
    private PlayerConfiguration playerConfig;
    public Vector2 MovementValue { get; private set; }  // ES-2024.01.07: Variable para el movimiento que leerá la StateMachine
    public bool isAttacking { get; private set; }
    public int attackIndex { get; private set; }
    public bool isBlocking { get; private set; }
   public bool isDodging { get; private set; }

    [SerializeField] private MeshRenderer playerMesh;

    private Players controls;  //Gamepads

    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();
        //var movers = FindObjectsOfType<Mover>();

        //var index = playerInput.playerIndex;
        //mover = GetComponent<Mover>();
        //mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        mover = GetComponent<Mover>();
        controls = new Players();
    }


    // Start is called before the first frame update
    void Start()
    {
        var joysticks = Input.GetJoystickNames();
        foreach (var gamepad in joysticks)
        {
            Debug.Log("Gamepad: " + gamepad);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (mover != null)
    //    mover.SetInputVector(context.ReadValue<Vector2>()); // ES-2024.01.07: comentamos este Move
        MovementValue = context.ReadValue<Vector2>();       // ES-2024.01.07: Variable para el movimiento que leerá la StateMachine 
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
        }
        else if (context.canceled)
        {
            isAttacking = false;
        }
    }

    public void OnBlocking(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBlocking = true;
        }
        else if (context.canceled)
        {
            isBlocking = false;
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDodging = true;
        }
        else if (context.canceled)
        {
            isDodging = false;
        }
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerMesh.material = pc.PlayerMaterial; 
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.Gamepad.Movement.name)
        {
            OnMove(obj);
        }

        if (obj.action.name == controls.Gamepad.Dodge.name)
        {
            OnDodge(obj);
        }

        if (obj.action.name == controls.Gamepad.LPunch.name)
        {
            attackIndex = 0;
            OnAttack(obj);
        }

        if (obj.action.name == controls.Gamepad.HPunch.name)
        {
            attackIndex = 3;
            OnAttack(obj);
        }

        if (obj.action.name == controls.Gamepad.CastMagic.name)
        {
            attackIndex = 4;
            OnAttack(obj);
        }

        if (obj.action.name == controls.Gamepad.Shield.name)
        {
            OnBlocking(obj);
        }

    }
}
