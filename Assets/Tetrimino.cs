using TetrimimoType;

public enum TetriminoType
{
    I, O, S, Z, J, L, T
}

public class Tetrimino
{
    private IMachine machine;
    private TetriminoBase tetriminoBase;
    private (int, int) startGridSquarePosition;

    public Tetrimino(IMachine machine, TetriminoType tetriminoType, (int, int) startGridSquarePosition)
    {
        this.machine = machine;
        this.startGridSquarePosition = startGridSquarePosition;

        switch (tetriminoType)
        {
            case TetriminoType.I: tetriminoBase = new TetriminoI();  break;
            case TetriminoType.O: tetriminoBase = new TetriminoO(); break;
            case TetriminoType.S: tetriminoBase = new TetriminoS(); break;
            case TetriminoType.Z: tetriminoBase = new TetriminoZ(); break;
            case TetriminoType.J: tetriminoBase = new TetriminoJ(); break;
            case TetriminoType.L: tetriminoBase = new TetriminoL(); break;
            case TetriminoType.T: tetriminoBase = new TetriminoT(); break;
        }

        Draw();
    }

    public void MoveHorizontal(int value)
    {
        if (tetriminoBase.IsMoveHorizontal(value))
        {
            tetriminoBase.Move((value, 0));
        }

        if (tetriminoBase.IsMoveDown())
        {
            if (tetriminoBase.IsConfirm) tetriminoBase.IsConfirm = false;
        }
    }

    public void MoveVertical(int value)
    {
        if (tetriminoBase.IsMoveDown())
        {
            tetriminoBase.Move((0, value));
            if (tetriminoBase.IsConfirm) tetriminoBase.IsConfirm = false;
        }
        else
        {
            tetriminoBase.IsConfirm = true;
        }
    }

    public void Rotate()
    {
        tetriminoBase.Rotate();

        if (tetriminoBase.IsMoveDown())
        {
            if (tetriminoBase.IsConfirm) tetriminoBase.IsConfirm = false;
        }
    }

    public bool GetIsConfirm()
    {
        return tetriminoBase.IsConfirm;
    }

    private void Draw()
    {
        tetriminoBase.DrawTetrimino(machine, startGridSquarePosition);
    }
}
