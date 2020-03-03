using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public Transform HealthBar;
    public TextMeshPro TextMesh;

    public void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }

    public void SetHealthPointsPercentage(float percentage)
    {
        if (percentage > 100 || percentage < 0) return;
        Vector3 percentageVector = new Vector3(1, 1, 1);
        percentageVector[0] = percentage;
        HealthBar.localScale = percentageVector;
    }
}
