// 1. UnityEngineをUsingしてはならない
// 2. 他の.csを足さずこのファイルのみで完結させること

using UnityEngine.Diagnostics;

public class SampleApplication : UserApplication
{
    private IMachine machine;
    private PlayerInputControl playerInputControl;
	private bool isOneced = false;
    private int fallCount = 0;
    private Tetrimino currentTetrimino;
    private FieldGridSquareList gridSquareList;

	// 毎フレーム(=1/60秒間隔で)呼ばれる
	public override void Update(IMachine machine)
	{
		Start(machine);
        FallTetrimino();
        PlayerControl();
        GenerateTetrimino();

        if (currentTetrimino.GetIsConfirm())
        {
            Next();
        }
    }

	private void Start(IMachine machine)
	{
        if (isOneced == false)
        {
            this.machine = machine;
            new FieldGridSquareList();
            new CreateField(machine);
            playerInputControl = new PlayerInputControl(machine);
            gridSquareList = FieldGridSquareList.instance;
            GenerateTetrimino();
        }

        isOneced = true;
    }

    private void GenerateTetrimino()
    {
        if (currentTetrimino == null)
        {
            currentTetrimino = new Tetrimino(machine, TetriminoType.O, (1, 2));
        }
    }

    private void Next()
    {
        Split();
        currentTetrimino = null;
        GenerateTetrimino();
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

    private void MoveTetrimino((int, int) vector)
    {
        if (currentTetrimino != null)
        {
            currentTetrimino.MoveVertical(vector.Item2);
            currentTetrimino.MoveHorizontal(vector.Item1);
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

    // deletate blocks
    private void Split()
    {
        FieldGridSquare[] gridSquares = new FieldGridSquare[ConstList.FIELDHORIZONTAL];

        for (int i = 0; i < ConstList.FIELDVERTICAL; i++)
        {
            int count = 0;

            for (int j = 0; j < ConstList.FIELDHORIZONTAL; j++)
            {
                FieldGridSquare gs = gridSquareList.GetGridSquarePositionOf((j, i));

                if (gs.IsInBlock)
                {
                    UnityEngine.Debug.Log(count);
                    gridSquares[count] = gs;
                    count++;

                    if (count == ConstList.FIELDHORIZONTAL)
                    {
                        UnityEngine.Debug.Log("Deleate");
                        foreach (var gridSquare in gridSquares)
                        {
                            gridSquare.Clear();
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
