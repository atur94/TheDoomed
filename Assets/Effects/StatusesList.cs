using System;

[Serializable]
public struct Disables
{
    public bool isStunned;

    public void Reset()
    {
        isStunned = false;
    }
}