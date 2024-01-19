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
    }
}
