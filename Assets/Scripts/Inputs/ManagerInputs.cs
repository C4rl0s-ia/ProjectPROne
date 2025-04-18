using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class ManagerInputs : MonoBehaviour
{
    private float inputX;

    public event Action OneButtonEvent;
    private bool isDown;
    
    public bool IsDown()
    {
        return isDown;
    }
    public void SetInputMove(InputAction.CallbackContext value) 
    {
        inputX = value.ReadValue<Vector2>().x;        
    }

    public float GetInputX()
    {
        return inputX;
    }

    public void OneButtonDown()
    {
        OneButtonEvent?.Invoke();   
    }
    public void GetButtonDown(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            OneButtonDown();
        }

        isDown = value.performed;   
    }

}
