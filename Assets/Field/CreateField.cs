
public class CreateField
{
    private IMachine machine;
    private (int, int) flameStartPixelPosition = new(0, 0);
    private (int, int) gridSquareStartPixlPosition = new(0, 0);
    private readonly int totalBlockSize = ConstList.BLOCKSIZE + ConstList.GRIDSQUAREGAP;

    public CreateField(IMachine machine)
    {
        this.machine = machine;
        CreateFieldFrame();
        CreateFieldGridSquares();
    }

    public void CreateFieldFrame()
    {
        (int, int) underStartPosition = new (flameStartPixelPosition.Item1 + totalBlockSize, flameStartPixelPosition.Item2);
        (int, int) rightStartPosition = new (0, 0);
        
        // under line
        for (int i = 0; i < ConstList.FIELDHORIZONTAL; i++)
        {
            int h = i * totalBlockSize + underStartPosition.Item1;

            (int, int) pos = new (h, flameStartPixelPosition.Item2);
            SquarePixel.DrawSquarePixel(pos.Item1, pos.Item2, PixelColors.color_White, machine);

            rightStartPosition = new (pos.Item1 + totalBlockSize, flameStartPixelPosition.Item2);
        }

        // side line
        for (int i = 0; i < ConstList.FIELDVERTICAL; i++)
        {
            int v = i * totalBlockSize;

            (int, int) posl = new (flameStartPixelPosition.Item1, v);
            (int, int) posr = new (rightStartPosition.Item1, v);

            SquarePixel.DrawSquarePixel(posl.Item1, posl.Item2, PixelColors.color_White, machine);
            SquarePixel.DrawSquarePixel(posr.Item1, posr.Item2, PixelColors.color_White, machine);

            gridSquareStartPixlPosition = posl;
        }
    }

    // フィールド左上からマスを作る
    public void CreateFieldGridSquares()
    {
        gridSquareStartPixlPosition = (
            gridSquareStartPixlPosition.Item1 + totalBlockSize, 
            gridSquareStartPixlPosition.Item2 + totalBlockSize);

        int count = 0;

        for (int v = 0; v < ConstList.FIELDVERTICAL; v++)
        {
            for (int h = 0;  h < ConstList.FIELDHORIZONTAL; h++)
            {
                (int, int) originPixelPos = new (gridSquareStartPixlPosition.Item1 + h * totalBlockSize, gridSquareStartPixlPosition.Item2 - v * totalBlockSize);
                (int, int) gridSquarePos = new (h, v);
                FieldGridSquare gridSquare = new FieldGridSquare(gridSquarePos, originPixelPos, machine);
                FieldGridSquareList.instance.Add(count, gridSquare);
                count++;
            }
        }
    }
}
