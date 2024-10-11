
public class Block
{
    private IMachine machine;
    private FieldGridSquare currentGridSquare;
    private readonly byte[] color;

    public Block(IMachine machine, FieldGridSquare gridSquare, byte[] color)
    {
        this.machine = machine;
        this.color = color;
        DrawBlock(gridSquare);
    }

    public void DrawBlock(FieldGridSquare gridSquare)
    {
        if (currentGridSquare != null)
        {
            EraseBlock();
        }

        currentGridSquare = gridSquare;

        SquarePixel.DrawSquarePixel(
            gridSquare.GetOriginalPixelPosition().Item1, gridSquare.GetOriginalPixelPosition().Item2,
            color, machine);
    }

    private void EraseBlock()
    {
        byte[] eraseColor = PixelColors.color_Black;

        SquarePixel.DrawSquarePixel(
            currentGridSquare.GetOriginalPixelPosition().Item1, currentGridSquare.GetOriginalPixelPosition().Item2,
            eraseColor, machine);
    }

    public (int, int) GetPosition()
    {
        return currentGridSquare.GetGridSquarePosition();
    }
}
