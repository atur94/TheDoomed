using System.Runtime.CompilerServices;

public class PlayerController : Controller
{
    protected override void Start()
    {
        base.Start();
        IsPlayer = true;
        AddToPlayersList();
    }

    private void AddToPlayersList()
    {
        if (character != null)
        {
            gameManager.PlayersList.Add(character);
        }
    }
}