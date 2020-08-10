using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickComponent : MonoBehaviour
{
    protected virtual void Tick()
    {

    }
    protected virtual void TickLate()
    {

    }
    protected virtual void TickFixed()
    {

    }
    protected virtual void InitializedObj()
    {

    }
    private void Awake()
    {
        InitializedObj();
    }
    private void Update()
    {
        Tick();
    }
    private void LateUpdate()
    {
        TickLate();
    }
    private void FixedUpdate()
    {
        TickFixed();
    }
}
