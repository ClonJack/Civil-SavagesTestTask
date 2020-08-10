using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IGameInput
{
    bool IsSit { get; }
    bool IsJump { get; }

}
public class InputComponent : IGameInput
{
    private bool isSit;
    private bool isJump;
    public bool IsSit => isSit;
    public bool IsJump => isJump;
    public InputComponent(GameInput inputActions)
    {
        BindKey(inputActions);
    }
    public void BindKey(GameInput inputActions)
    {
        inputActions.Player.Sit.started += ctx => isSit = true;
        inputActions.Player.Sit.canceled += ctx => isSit = false;

        inputActions.Player.Jump.started += ctx => isJump = true;
        inputActions.Player.Jump.canceled += ctx => isJump = false;
    }
}
