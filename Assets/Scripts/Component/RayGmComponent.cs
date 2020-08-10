using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IRayGame
{
    string GetPointTag();
}
public class RayGmComponent : IRayGame
{
    private Transform beginPoint;
    public RayGmComponent(Transform beginPoint)
    {
        this.beginPoint = beginPoint;
    }
    public string GetPointTag()
    {
        RaycastHit raycast;
        var ray = new Ray(beginPoint.position, beginPoint.TransformDirection(Vector3.forward));

        Debug.DrawRay(beginPoint.position, beginPoint.TransformDirection(Vector3.forward) * 2.5f, Color.yellow);

        if (Physics.Raycast(ray, out raycast, 2.5f))
            return raycast.transform.tag;

        return string.Empty;
    }
}
