using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IWidget
{
    Vector2 LookTarget(Image img, Camera cam, Transform target, Transform pl);
}
public class UIComponent : IWidget
{
    public Vector2 LookTarget(Image img, Camera cam, Transform target, Transform pl)
    {
        var minVal = new Vector2(img.rectTransform.rect.width / 2, img.rectTransform.rect.height / 2);
        var maxVal = new Vector2(Screen.width - minVal.x, Screen.height - minVal.y);

        var pos = cam.WorldToScreenPoint(target.position);

        if (Vector3.Dot(target.position - pl.position, pl.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
                pos.x = maxVal.x;
            else
                pos.x = maxVal.x;
        }
        pos.x = Mathf.Clamp(pos.x, minVal.x, maxVal.x);
        pos.y = Mathf.Clamp(pos.y, minVal.y, maxVal.y);

        return pos;
    }
}
