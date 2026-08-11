using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }
    private InputActions inputActions;
    public event EventHandler onPauseButtonPressed;
    private void Awake()
    {
        instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onPauseButtonPressed?.Invoke(this,EventArgs.Empty);
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }
    public bool isUpLanderPressed()
    {
        return inputActions.Player.LanderUp.IsPressed();
    }
    public bool isLeftLanderPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }
    public bool isRightLanderPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }
    public bool isMiniMapPressed() { 
        return inputActions.Player.MiniMap.WasPressedThisFrame();
    }

}