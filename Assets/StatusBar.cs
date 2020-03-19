using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public Transform HealthBar;
    public Transform ManaBar;
    public Transform BlackHpBar;
    public Transform BlackManaBar;
    public TextMeshPro TextMesh;
    public TextMeshPro HealthText;
    public TextMeshPro ManaText;

    public void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void SetHealthPointsPercentage(float percentage)
    {
        if (percentage > 100 || percentage < 0) return;
        Vector3 percentageVector = new Vector3(1, 1, 1);
        percentageVector.x = percentage;
        HealthBar.localScale = percentageVector;
        BlackHpBar.localPosition = new Vector3(percentage-0.5f, 0f, 0f);
    }

    public void SetManaPointPercentage(float percentage)
    {
        if (percentage > 100 || percentage < 0) return;
        Vector3 percentageVector = new Vector3(1, 1, 1);
        percentageVector.x = percentage;
        ManaBar.localScale = percentageVector;
        var localPosition = BlackManaBar.localPosition;
        localPosition = new Vector3(percentage - 0.5f, localPosition.y, localPosition.z);
        BlackManaBar.localPosition = localPosition;
    }

    public void SetHealthPoints(float currentHealth, float maxHealth)
    {
        float percentage = currentHealth / maxHealth;

        SetHealthPointsPercentage(percentage);
        HealthText.SetText($"hp: {maxHealth}/{currentHealth}");
    }

    public void SetManaPoints(float currentMana, float maxMana)
    {
        float percentage = currentMana / maxMana;

        SetManaPointPercentage(percentage);
        ManaText.SetText($"mp: {maxMana}/{currentMana}");
    }
}
