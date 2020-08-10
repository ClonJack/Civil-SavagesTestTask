using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface IUtilitCharacterController
{
    bool IsGrounded(Transform player, float distanceToGround);
}
public class UtilitCharacterControllerComponent : IUtilitCharacterController
{
    public bool IsGrounded(Transform player, float distanceToGround)
    {
        var ray = new Ray(player.position, player.TransformDirection(Vector3.down));
        var rayhit = new RaycastHit();

        Debug.DrawRay(ray.origin, ray.direction * distanceToGround, Color.green);

        return Physics.Raycast(ray, out rayhit, distanceToGround);
    }
}
