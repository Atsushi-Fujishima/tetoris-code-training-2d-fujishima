
public class FieldGridSquare
{
    // 座標にブロックがあるかどうか
    private Block inBlock = null;
    private byte[] color = new byte[3];
    private (int, int) gridSquarePosition;
    private (int, int) originalPixelPosition;
    private IMachine machine;

    public Block InBlock
    {
        set { inBlock = value; }
        get { return inBlock; }
    }

    public FieldGridSquare((int, int) gridSquarePosition, (int, int) originalPixelPosition, IMachine machine)
    {
        inBlock = null;
        color = PixelColors.color_Black;
        this.machine = machine;
        this.gridSquarePosition = gridSquarePosition;
        this.originalPixelPosition = originalPixelPosition;
        CreatePixelsPosition(color);
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
        inBlock = null;
        color = PixelColors.color_Black;
        CreatePixelsPosition(color);
    }

    private void CreatePixelsPosition(byte[] color)
    {
        SquarePixel.DrawSquarePixel(
            originalPixelPosition.Item1, originalPixelPosition.Item2,
            color, machine);
    }
}
