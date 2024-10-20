
namespace TetrimimoType 
{
    public class TetriminoJ : TetriminoBase
    {
        public override void DrawTetrimino(IMachine machine, (int, int) startGridSquarePosition)
        {
            color = PixelColors.color_Blue;

            (int, int) setGridSquarePosition = startGridSquarePosition;
            blocks[0] = new Block(machine, setGridSquarePosition, color);

            setGridSquarePosition = new(startGridSquarePosition.Item1 - 1, startGridSquarePosition.Item2);
            blocks[1] = new Block(machine, setGridSquarePosition, color);

            setGridSquarePosition = new(startGridSquarePosition.Item1 - 1, startGridSquarePosition.Item2 + 1);
            blocks[2] = new Block(machine, setGridSquarePosition, color);

            setGridSquarePosition = new(startGridSquarePosition.Item1 + 1, startGridSquarePosition.Item2);
            blocks[3] = new Block(machine, setGridSquarePosition, color);
        }
    }
}