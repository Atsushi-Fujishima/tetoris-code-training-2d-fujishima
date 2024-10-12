
public class FieldGridSquareList
{
    static public FieldGridSquareList instance;
    private FieldGridSquare[] gridSquareList = null;

    // range
    public readonly (int, int) columnsRange = new(0, ConstList.FIELDVERTICAL - 1);
    public readonly (int, int) rowsRange = new(0, ConstList.FIELDHORIZONTAL - 1);

    public FieldGridSquareList()
    {
        if (instance == null) 
        {
            instance = this;

            int allSquare = ConstList.FIELDVERTICAL * (ConstList.FIELDHORIZONTAL + ConstList.SIDEFLAMENUM);
            int flameSquare = (ConstList.FIELDVERTICAL * 2);
            gridSquareList = new FieldGridSquare[allSquare - flameSquare];
        }
    }

    public void Add(int index, FieldGridSquare gridSquare)
    {
        gridSquareList[index] = gridSquare;
    }

    //XXX: ’v–½“I‚ÈŒ‡Š× ”z—ñ‚Ì’·‚³‚Æ—v‘f‚ª‹l‚ß‚ç‚ê‚È‚¢
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

    public FieldGridSquare[] GetList()
    {
        return gridSquareList;
    } 
}
