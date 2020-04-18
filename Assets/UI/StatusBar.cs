using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StatusBar : MonoBehaviour
{
    public Vector3 offset;
    public GameObject trackingObject;
    public Slider healthBar;
    public Slider manaBar;
    public Slider attackCooldown;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI manaLabel;

    private void Start()
    {
        trackingObject = gameObject.transform.parent.gameObject.transform.parent.gameObject;
    }

    public void InitValues()
    {
        healthBar.value = 1f;
        manaBar.value = 1f;
        attackCooldown.value = 1f;
    }
    private void Update()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackingObject.transform.position) + offset;
    }

    public void SetHealthPoints(float current, float max)
    {
        float percentage = current / max;
        if (percentage > 100 || percentage < 0) return;
        healthBar.value = percentage;
        healthLabel.SetText($"{max}/{current}");

    }

    public void SetManaPoints(float current, float max)
    {
        float percentage = current / max;
        if (percentage > 100 || percentage < 0) return;
        manaBar.value = percentage;
        manaLabel.SetText($"{max}/{current}");
    }

    public void SetAttackCooldown(float percentage)
    {
        if (percentage > 100 || percentage < 0) return;
        attackCooldown.value = percentage;
    }
}
