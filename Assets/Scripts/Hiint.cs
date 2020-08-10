using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiint : MonoBehaviour
{
    public string gmHintName;
    public void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player pl))
            pl.DisplayHint(gmHintName);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player pl))
            pl.ClearHint();
    }

}
