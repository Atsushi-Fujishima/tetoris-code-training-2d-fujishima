// 1. UnityEngineをUsingしてはならない
// 2. 他の.csを足さずこのファイルのみで完結させること

public class SampleApplication : UserApplication
{
	private IMachine machine;
	private FieldGridSquareList gridSquareList;

	private bool isOneced = false;
    private Tetrimino currentTetrimino;

	// 毎フレーム(=1/60秒間隔で)呼ばれる
	public override void Update(IMachine machine)
	{
		Start(machine);

        // sample code
        if (machine.Up)
		{
            
        }
		else
		{
            
        }

		if (machine.Down)
		{
            
        }

		if (machine.Left)
		{
			if (currentTetrimino != null)
            {
                currentTetrimino.MoveHorizontal(-1);
            }
		}

        if (machine.Right)
        {
            if (currentTetrimino != null)
            {
                currentTetrimino.MoveHorizontal(1);
            }
        }
    }

	private void Start(IMachine machine)
	{
        if (isOneced == false)
        {
			this.machine = machine;
            gridSquareList = new FieldGridSquareList();
            new CreateField(this.machine, gridSquareList);

            currentTetrimino = new Tetrimino(machine, gridSquareList, TetriminoType.Z, (1, 2));

            //new Block(machine, gridSquareList.GetGridSquarePositionOf((0, 1)), PixelColors.color_Cyan);

            if (currentTetrimino != null)
            {
                //currentTetrimino.MoveHorizontal(1);
            }
        }

        isOneced = true;
    }
}
