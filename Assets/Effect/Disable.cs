using System;
using UnityEngine;

public abstract class Disable
{
    public bool CanBeRemoved = false;
    public bool Stackable = false;
    public int Id;
    public float Duration = 1f;
    public float TimeLeft { get; set; }
    public float Chance = 1f;
    protected GameCharacter gameCharacter;

    protected Disable(ref GameCharacter gameCharacter)
    {
        this.gameCharacter = gameCharacter;
    }

    void Start()
    {
        TimeLeft = Duration;
        Init();
        OnDisableStart();
    }

    protected virtual void Init()
    {
    }

    protected virtual void OnDisableStart() { }
    protected virtual void OnDisableEnd() { }
    protected virtual void OnDisableDuring() { }

    public void Update(float time)
    {
        if (CanBeRemoved) return;
        if (TimeLeft > time)
        {
            TimeLeft -= time;
            OnDisableDuring();
        }
        else
        {
            TimeLeft = 0;
            CanBeRemoved = true;
            OnDisableDuring();
            OnDisableEnd();
        }
    }
}

public enum EffectType
{
    Slow,
    Stun,
    Freeze,
    Silence,
    Sleep,
    ForcedMovement,
    Root,
    Taunte,
    Hide,
    Disarm,
    Fear,
    Blind
}

