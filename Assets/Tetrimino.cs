using TetrimimoType;
using static Unity.Collections.AllocatorManager;

public enum TetriminoType
{
    I, O, S, Z, J, L, T
}

public class Tetrimino
{
    private TetriminoType tetriminoType = TetriminoType.O;
    private IMachine machine;
    private FieldGridSquareList gridSquareList;
    private Block[] blocks;
    private TetriminoBase tetriminoBase = new TetriminoBase();
    private (int, int) startGridSquarePosition;

    public Tetrimino(IMachine machine, FieldGridSquareList gridSquareList, TetriminoType tetriminoType, (int, int) startGridSquarePosition)
    {
        this.machine = machine;
        this.gridSquareList = gridSquareList;
        this.tetriminoType = tetriminoType;
        this.startGridSquarePosition = startGridSquarePosition;

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
        foreach (var block in blocks)
        {
            int set = block.GetPosition().Item1 + value;
            if (set < 0 || set > 9)
            {
                return;
            }
        }

        foreach (var block in blocks)
        {
            block.DrawBlock(gridSquareList.GetGridSquarePositionOf((block.GetPosition().Item1 + value, block.GetPosition().Item2)));
        }
    }

    private void Draw()
    {
        tetriminoBase.DrawTetrimino(machine, gridSquareList, startGridSquarePosition);
        blocks = tetriminoBase.Blocks;
    } 
}
