public class GameDeveloper : PlayerMove, ICharacter
{
    public string Name() => _name;
    private void OnEnable()
    {
        SendToCharList(this);
    }
    public void CharacterInit()
    {
        base.InitShoot();
    }
    public void CharacterLoop(InputHandler input)
    {
        MoveAndTurnLoop(input.Turn, input.Move, input.Special);
        ShootUpdate();
    }
}