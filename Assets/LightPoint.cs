using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightPoint : MonoBehaviour
{
    public Transform target;

    public Light light;
    private void LateUpdate()
    {
        if (light == null || target == null) return;
        light.transform.LookAt(target);
    }
}
