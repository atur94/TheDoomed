using System;
using UnityEngine;

public abstract class Status
{
    public bool CanBeRemoved = false;
    public bool Stackable = false;
    public int Id;
    public float Duration = 1f;
    public float TimeLeft { get; set; }
    protected GameCharacter gameCharacter;

    protected Status(ref GameCharacter gameCharacter, Status status)
    {
        this.gameCharacter = gameCharacter;
        Duration = status.Duration;
        TimeLeft = status.Duration;
        Id = status.Id;
        Stackable = status.Stackable;
    }

    protected Status(ref GameCharacter gameCharacter, float duration, int id, bool stackable = false)
    {
        this.gameCharacter = gameCharacter;
        this.Duration = duration;
        this.TimeLeft = duration;
        id = 1;
        Stackable = stackable;
    }

    void Start()
    {
        TimeLeft = Duration;
        Init();
        OnStatusStart();
    }

    protected virtual void Init()
    {
    }

    protected virtual void OnStatusStart() { }
    protected virtual void OnStatusEnd() { }
    protected virtual void OnStatusDuring() { }

    public void Update(float time)
    {
        if (CanBeRemoved) return;
        if (TimeLeft > time)
        {
            TimeLeft -= time;
            OnStatusDuring();
        }
        else
        {
            TimeLeft = 0;
            CanBeRemoved = true;
            OnStatusDuring();
            OnStatusEnd();
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

