
public class FieldGridSquare
{
    private (int, int) gridSquarePosition;
    private (int, int) originalPixelPosition;
    private IMachine machine;

    public FieldGridSquare((int, int) gridSquarePosition, (int, int) originalPixelPosition, IMachine machine)
    {
        this.machine = machine;
        this.gridSquarePosition = gridSquarePosition;
        this.originalPixelPosition = originalPixelPosition;
        CreatePixelsPosition();
    }

    public (int, int) GetGridSquarePosition()
    {
        return gridSquarePosition;
    }

    public (int, int) GetOriginalPixelPosition()
    {
        return originalPixelPosition;
    }

    private void CreatePixelsPosition()
    {
        SquarePixel.DrawSquarePixel(
            originalPixelPosition.Item1, originalPixelPosition.Item2,
            PixelColors.color_DarkBlue, machine);
    }
}
