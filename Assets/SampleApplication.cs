// 1. UnityEngineをUsingしてはならない
// 2. 他の.csを足さずこのファイルのみで完結させること

using System;

public class SampleApplication : UserApplication
{
    private IMachine machine;
    private PlayerInputControl playerInputControl;
	private bool isOneced = false;
    private int fallCount = 0;
    private Tetrimino currentTetrimino;
    private FieldGridSquareList gridSquareList;
    private FrameEffect frameEffect;
    private int countFlame = 0;
    private bool isGameOver = false;

	// 毎フレーム(=1/60秒間隔で)呼ばれる
	public override void Update(IMachine machine)
	{
        if (isGameOver == false)
        {
            Start(machine);
            GenerateTetrimino();
            FallTetrimino();
            PlayerControl();

            if (currentTetrimino != null && currentTetrimino.GetIsConfirm())
            {
                countFlame++;
                if (countFlame >= 20)
                {
                    if (currentTetrimino.GetIsConfirm() == false)
                    {
                        countFlame = 0;
                    }

                    countFlame = 0;
                    Next();
                }
            }
        }

        if (frameEffect.isFlashingDelete) frameEffect.FlashFrameDelete(); 
        if (frameEffect.isFlashingGameOver) frameEffect.FlashFrameGameOver();
    }

	private void Start(IMachine machine)
	{
        if (isOneced == false)
        {
            this.machine = machine;
            new FieldGridSquareList();
            var createField = new CreateField(machine);
            frameEffect = new FrameEffect(createField);
            playerInputControl = new PlayerInputControl(machine);
            gridSquareList = FieldGridSquareList.instance;
            GenerateTetrimino();
        }

        isOneced = true;
    }

    private void GenerateTetrimino()
    {
        var setPosition = (5, 2);

        if (currentTetrimino == null)
        {
            if (gridSquareList.GetGridSquarePositionOf((setPosition.Item1, 1)).InBlock != null)
            {
                GameOver();
                return;
            }

            TetriminoType tetriminoType = RandomTetrimino();
            currentTetrimino = new Tetrimino(machine, tetriminoType, setPosition);
        }
    }

    private TetriminoType RandomTetrimino()
    {
        Random random = new Random();
        int r = random.Next(0, Enum.GetValues(typeof(TetriminoType)).Length);
        Array blockTypeValues = Enum.GetValues(typeof(TetriminoType));
        TetriminoType setType = (TetriminoType)blockTypeValues.GetValue(r);
        return setType;
    }

    private void Next()
    {
        DeleateTetrimino();
        currentTetrimino = null;
        GenerateTetrimino();
    }

    private void GameOver()
    {
        isGameOver = true;
        frameEffect.StartFrameFlashGameOver();

        foreach (var grid in gridSquareList.GetList())
        {
            grid.Clear();
        }
    }

    private void PlayerControl()
    {
        if (playerInputControl.UpKeyWasPressed())
        {
            RotateTetrimino();
        }

        if (playerInputControl.DownKey() || playerInputControl.DownKeyWasPressed())
        {
            if (currentTetrimino != null)
            {
                currentTetrimino.MoveVertical(1);
            }
        }
        else if (playerInputControl.LeftKey() || playerInputControl.LeftKeyWasPressed())
        {
            if (currentTetrimino != null)
            {
                currentTetrimino.MoveHorizontal(-1);
            }
        }
        else if (playerInputControl.RightKey() || playerInputControl.RightKeyWasPressed())
        {
            if (currentTetrimino != null)
            {
                currentTetrimino.MoveHorizontal(1);
            }
        }
    }

    private void RotateTetrimino()
    {
        if (currentTetrimino != null)
        {
            currentTetrimino.Rotate();
        }
    }

    private void FallTetrimino()
    {
        if (fallCount < ConstList.FLAMECOUNT)
        {
            fallCount++;
        }
        else
        {
            fallCount = 0;
            currentTetrimino.MoveVertical(1);
        }
    }

    private void DeleateTetrimino()
    {
        FieldGridSquare[] gridSquares = new FieldGridSquare[ConstList.FIELDHORIZONTAL];
        bool isDelete = false;
        int underColumns = 0;
        int deleteColumnsCount = 0;

        for (int i = 0; i < ConstList.FIELDVERTICAL; i++)
        {
            int count = 0;
            

            for (int j = 0; j < ConstList.FIELDHORIZONTAL; j++)
            {
                FieldGridSquare gs = gridSquareList.GetGridSquarePositionOf((j, i));

                if (gs.InBlock != null)
                {
                    gridSquares[count] = gs;
                    count++;

                    if (count == ConstList.FIELDHORIZONTAL)
                    {
                        deleteColumnsCount++;
                        underColumns = (underColumns < i) ? i : underColumns;
                        foreach (var gridSquare in gridSquares)
                        {
                            gridSquare.Clear();
                        }

                        isDelete = true;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if (isDelete)
        {
            Split(underColumns, deleteColumnsCount);
            if (deleteColumnsCount > 1) frameEffect.StartFrameFlashDelete();
        }
        
    }

    private void Split(int columns, int deleteColumnsCount)
    {
        for (int i = columns; i > -1; i--)
        {
            for (int j = 0; j < ConstList.FIELDHORIZONTAL; j++)
            {
                (int, int) pos = (j, i);
                var grid = gridSquareList.GetGridSquarePositionOf(pos);
                if (grid.InBlock != null && i < gridSquareList.columnsRange.Item2)
                {
                    var underGrid = gridSquareList.GetGridSquarePositionOf((pos.Item1, SplitDepth(i, deleteColumnsCount)));
                    byte[] color = grid.InBlock.Color;
                    grid.Clear();
                    underGrid.InBlock = new Block(machine, underGrid.GetGridSquarePosition(), color);
                }
            }
        }
    }

    private int SplitDepth(int columns, int deleteColumnsCount)
    {
        int depth = columns + 1;

        for (int n = 0; n < deleteColumnsCount; n++)
        {
            if (depth < gridSquareList.columnsRange.Item2)
            {
                depth = columns + 1 + n;
            }
            else
            {
                break;
            }
        }

        return depth;
    }
}
