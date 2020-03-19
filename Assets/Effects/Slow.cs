
using System.Reflection.Emit;

public class Slow : Status
{
    public float movementSpeedPercent = 0f;

    public Slow(GameCharacter gameCharacter, float slow, float duration, int id, bool stackable = false) : base(ref gameCharacter, duration, id, stackable)
    {
        movementSpeedPercent = slow;
    }

    public Slow(ref GameCharacter gameCharacter, float slow,Status status) : base(ref gameCharacter, status)
    {
        movementSpeedPercent = slow;
    }

    protected override void OnStatusDuring()
    {
        gameCharacter.movmentSpeedCalculation += movementSpeedPercent;
    }
}

