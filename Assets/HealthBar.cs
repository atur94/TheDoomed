using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform _healthBarPanel;

    public void Start()
    {
        var componenets = GetComponentsInChildren<Transform>();
        foreach (var componenet in componenets)
        {
            if (componenet.tag.Equals("HealthBar"))
            {
                _healthBarPanel = componenet;
                return;
            }
        }
    }

    public void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }

    public void SetHealthPointsPercentage(float percentage)
    {
        if (percentage > 100 || percentage < 0) return;
        Vector3 percentageVector = new Vector3(1, 1, 1);
        percentageVector[0] = percentage;
        _healthBarPanel.localScale = percentageVector;
    }
}
