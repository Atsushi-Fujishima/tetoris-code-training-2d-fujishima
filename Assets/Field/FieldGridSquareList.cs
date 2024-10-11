
public class FieldGridSquareList
{
    private FieldGridSquare[] gridSquareList = null;
    private readonly int allSquare = ConstList.FIELDVERTICAL * (ConstList.FIELDHORIZONTAL + ConstList.SIDEFLAMENUM);
    private readonly int flameSquare = (ConstList.FIELDVERTICAL * 2) + ConstList.FIELDHORIZONTAL;

    // range
    public readonly (int, int) columnsRange = new(0, ConstList.FIELDVERTICAL - 1);
    public readonly (int, int) rowsRange = new(0, ConstList.FIELDHORIZONTAL - 1);

    public FieldGridSquareList()
    {
        gridSquareList = new FieldGridSquare[allSquare * flameSquare];
    }

    public void Add(int index, FieldGridSquare gridSquare)
    {
        gridSquareList[index] = gridSquare;
    }

    //XXX: 致命的な欠陥 配列の長さと要素が詰められない
    public void EasyRemove(int index)
    {
        gridSquareList[index] = null;
    }

    public FieldGridSquare GetGridSquare(int index)
    {
        return gridSquareList[index];
    }

    public FieldGridSquare GetGridSquarePositionOf((int, int) gridSquarePosition)
    {
        foreach (var gridSquare in gridSquareList)
        {
            if (gridSquare.GetGridSquarePosition().Item1 == gridSquarePosition.Item1 && gridSquare.GetGridSquarePosition().Item2 == gridSquarePosition.Item2)
            {
                return gridSquare;
            }
        }

        return null;
    }
}
