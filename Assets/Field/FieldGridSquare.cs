
public class FieldGridSquare
{
    // 座標にブロックがあるかどうか
    private bool isInBlock = false;

    private (int, int) gridSquarePosition;
    private (int, int) originalPixelPosition;
    private IMachine machine;

    public bool IsInBlock
    {
        set { isInBlock = value; }
        get { return isInBlock; }
    }

    public FieldGridSquare((int, int) gridSquarePosition, (int, int) originalPixelPosition, IMachine machine)
    {
        isInBlock = false;
        this.machine = machine;
        this.gridSquarePosition = gridSquarePosition;
        this.originalPixelPosition = originalPixelPosition;
        CreatePixelsPosition(PixelColors.color_DarkBlue);
    }

    public (int, int) GetGridSquarePosition()
    {
        return gridSquarePosition;
    }

    public (int, int) GetOriginalPixelPosition()
    {
        return originalPixelPosition;
    }

    public void Clear()
    {
        isInBlock = false;
        CreatePixelsPosition(PixelColors.color_Black);
    }

    private void CreatePixelsPosition(byte[] color)
    {
        SquarePixel.DrawSquarePixel(
            originalPixelPosition.Item1, originalPixelPosition.Item2,
            color, machine);
    }
}
