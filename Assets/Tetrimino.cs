using TetrimimoType;

public enum TetriminoType
{
    I, O, S, Z, J, L, T
}

public class Tetrimino
{
    private TetriminoType tetriminoType = TetriminoType.O;
    private IMachine machine;
    private FieldGridSquareList gridSquareList;
    private TetriminoBase tetriminoBase;
    private (int, int) startGridSquarePosition;

    public Tetrimino(IMachine machine, TetriminoType tetriminoType, (int, int) startGridSquarePosition)
    {
        this.machine = machine;
        this.tetriminoType = tetriminoType;
        this.startGridSquarePosition = startGridSquarePosition;
        gridSquareList = FieldGridSquareList.instance;

        switch (tetriminoType)
        {
            case TetriminoType.I: break;
            case TetriminoType.O: tetriminoBase = new TetriminoO(); break;
            case TetriminoType.S: break;
            case TetriminoType.Z: tetriminoBase = new TetriminoZ(); break;
            case TetriminoType.J: break;
            case TetriminoType.L: break;
            case TetriminoType.T: break;
        }

        Draw();
    }

    public void MoveHorizontal(int value)
    {
        foreach (var block in tetriminoBase.Blocks)
        {
            if (value < 0)
            {
                if (block.GetCurrentGridSquare().GetGridSquarePosition().Item1 <= gridSquareList.rowsRange.Item1)
                {
                    return;
                }
            }
            else
            {
                if (block.GetCurrentGridSquare().GetGridSquarePosition().Item1 >= gridSquareList.rowsRange.Item2)
                {
                    return;
                }
            }
        }

        tetriminoBase.Move((value, 0));
    }

    public void MoveVertical(int value)
    {
        foreach (var block in tetriminoBase.Blocks)
        {
            if (block.GetGridSquarePosition().Item2 >= gridSquareList.columnsRange.Item2)
            {
                return;
            }
        }

        tetriminoBase.Move((0, value));
    }

    private void Draw()
    {
        tetriminoBase.DrawTetrimino(machine, startGridSquarePosition);
    } 
}
