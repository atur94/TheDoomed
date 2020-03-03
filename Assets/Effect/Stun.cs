public class Stun : Disable
{
    public Stun(GameCharacter gameCharacter, float duration, int id, bool stackable = false) : base(ref gameCharacter)
    {
        this.Duration = duration;
        this.TimeLeft = duration;
        id = 1;
        Stackable = stackable;
    }

    public Stun(GameCharacter gameCharacter, Disable disable) : base(ref gameCharacter)
    {
        Duration = disable.Duration;
        TimeLeft = disable.Duration;
        Id = disable.Id;
        Stackable = disable.Stackable;
    }

    protected override void OnDisableDuring()
    {
        gameCharacter.isStunned = true;
    }
}

