
public class Block
{
    private IMachine machine;
    private FieldGridSquare gridSquare;
    private (int, int) gridSquarePosition;
    private readonly byte[] color;

    public byte[] Color { get { return color; } }

    public Block(IMachine machine, (int, int) setGridSquarePosition, byte[] color)
    {
        this.machine = machine;
        this.color = color;
        DrawBlock(setGridSquarePosition);
    }

    public void DrawBlock((int, int) setGridSquarePos)
    {
        gridSquarePosition = setGridSquarePos;
        gridSquare = FieldGridSquareList.instance.GetGridSquarePositionOf(setGridSquarePos);

        SquarePixel.DrawSquarePixel(
            gridSquare.GetOriginalPixelPosition().Item1, gridSquare.GetOriginalPixelPosition().Item2,
            color, machine);

        gridSquare.InBlock = this;
    }

    public void EraseBlock()
    {
        byte[] eraseColor = PixelColors.color_Black;

        SquarePixel.DrawSquarePixel(
            gridSquare.GetOriginalPixelPosition().Item1, gridSquare.GetOriginalPixelPosition().Item2,
            eraseColor, machine);

        gridSquare.InBlock = null;
    }

    public FieldGridSquare GetCurrentGridSquare()
    {
        return gridSquare;
    }

    public (int, int) GetGridSquarePosition()
    {
        return gridSquarePosition;
    }
}
