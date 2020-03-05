public class Stun : Status
{
    public Stun(GameCharacter gameCharacter, float duration, int id, bool stackable = false) : base(ref gameCharacter, duration, id, stackable)
    {
    }

    public Stun(GameCharacter gameCharacter, Status status) : base(ref gameCharacter, status)
    {

    }

    protected override void OnStatusDuring()
    {
        gameCharacter.isStunned = true;
    }
}

