using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICamera
{
    void Rotate(Vector2 dr);
}
public class CameraComponent : ICamera
{
    private Vector2 sensitivity = new Vector2(1.5f, 1.5f);

    private Transform camera;
    private Transform player;

    private float xDeg, yDeg;
    private float minX = -80, maxX = 70;
    public CameraComponent(Transform camera, Transform player)
    {
        this.camera = camera;
        this.player = player;
    }
    public void Rotate(Vector2 dr)
    {
        if (dr.sqrMagnitude < 0.01)
            return;

        yDeg -= dr.x * sensitivity.y;
        xDeg += dr.y * sensitivity.x;

        xDeg = Mathf.Clamp(xDeg, minX, maxX);

        player.transform.rotation = Quaternion.Euler(new Vector3(player.eulerAngles.x, -yDeg, player.eulerAngles.z));
        camera.rotation = Quaternion.Euler(-xDeg, camera.eulerAngles.y, camera.eulerAngles.z);
    }
}


