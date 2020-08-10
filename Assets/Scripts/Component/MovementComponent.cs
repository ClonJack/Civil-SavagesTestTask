using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    Vector3 JumpPlayer(Vector3 dir, bool isCanGravity, string nameCurrentRay, bool IsGrounded);
}
public class MovementComponent : IMove
{
    private float jumpPower = 15f;
    private float limitJump = -10.0f;
    private float vertSpeed;

    private IGameInput gameInput;
    public MovementComponent(IGameInput gameInput)
    {
        this.gameInput = gameInput;
    }
    public Vector3 JumpPlayer(Vector3 dir, bool isCanGravity, string nameCurrentRay,bool IsGrounded)
    {
        if (isCanGravity)
            return Vector3.one;

        if (IsGrounded)
        {
            if (gameInput.IsJump && nameCurrentRay == "JumpWall")
                vertSpeed = jumpPower;
        }
        else
        {
            vertSpeed += Physics.gravity.y * 5 * Time.deltaTime;

            if (vertSpeed < limitJump)
                vertSpeed = limitJump;
            else
            {
                if (nameCurrentRay == "JumpWall")
                {
                    dir.z = -5;
                    dir.x *= 5;
                }
            }
        }
        dir.y = vertSpeed;
        return dir;
    }
}
