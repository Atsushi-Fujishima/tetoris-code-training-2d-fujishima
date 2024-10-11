
public static class SquarePixel
{
    public static void DrawSquarePixel(int x, int y, byte[] color, IMachine machine, out (int, int)[] pixelsPosition) 
    {
        pixelsPosition = new (int, int)[ConstList.BLOCKSIZE * ConstList.BLOCKSIZE];

        for (int i = 0; i < ConstList.BLOCKSIZE; i++)
        {
            for (int j = 0; j < ConstList.BLOCKSIZE; j++)
            {
                int index = i + j;
                (int, int) position = new(x + i, y + j);
                pixelsPosition[index] = position;

                machine.Draw(position.Item1, position.Item2, color[0], color[1], color[2]);
            }
        }
    }

    public static void DrawSquarePixel(int x, int y, byte[] color, IMachine machine)
    {
        for (int i = 0; i < ConstList.BLOCKSIZE; i++)
        {
            for (int j = 0; j < ConstList.BLOCKSIZE; j++)
            {
                (int, int) position = new(x + i, y + j);

                machine.Draw(position.Item1, position.Item2, color[0], color[1], color[2]);
            }
        }
    }
}
