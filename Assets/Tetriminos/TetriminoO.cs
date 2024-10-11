
namespace TetrimimoType
{
    public class TetriminoO : TetriminoBase
    {
        public override void DrawTetrimino(IMachine machine, FieldGridSquareList gridSquareList, (int, int) startGridSquarePosition)
        {
            color = PixelColors.color_Yeloow;

            (int, int) setGridSquarePosition = startGridSquarePosition;
            blocks[0] = new Block(machine, gridSquareList.GetGridSquarePositionOf(setGridSquarePosition), color);

            setGridSquarePosition = new(startGridSquarePosition.Item1 + 1, startGridSquarePosition.Item2);
            blocks[1] = new Block(machine, gridSquareList.GetGridSquarePositionOf(setGridSquarePosition), color);

            setGridSquarePosition = new(startGridSquarePosition.Item1, startGridSquarePosition.Item2 - 1);
            blocks[2] = new Block(machine, gridSquareList.GetGridSquarePositionOf(setGridSquarePosition), color);

            setGridSquarePosition = new(startGridSquarePosition.Item1 + 1, startGridSquarePosition.Item2 - 1);
            blocks[3] = new Block(machine, gridSquareList.GetGridSquarePositionOf(setGridSquarePosition), color);
        }
    }

}