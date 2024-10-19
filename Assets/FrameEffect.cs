
public class FrameEffect
{
    private CreateField field;
    private int[] flameCounts = new int[3];
    private bool[] flashingFlag = new bool[4];
    public bool isFlashingDelete = false;
    public bool isFlashingGameOver = false;

    public FrameEffect(CreateField field)
    {
        this.field = field;
    }

    public void StartFrameFlashDelete()
    {
        Initialize();
        isFlashingDelete = true;
    }

    public void StartFrameFlashGameOver()
    {
        Initialize();
        isFlashingGameOver = true;
    }

    public void FlashFrameDelete()
    {
        if (flashingFlag[0] == false)
        {
            field.CreateFieldFrame(PixelColors.color_Cyan);
            flashingFlag[0] = true;
        }

        flameCounts[0]++;
        if (flameCounts[0] > 10)
        {
            if (flashingFlag[1] == false)
            {
                field.CreateFieldFrame(PixelColors.color_Yeloow);
                flashingFlag[1] = true;
            }
        }

        flameCounts[1]++;
        if (flameCounts[1] > 30)
        {
            if (flashingFlag[2] == false)
            {
                field.CreateFieldFrame(PixelColors.color_Cyan);
                flashingFlag[2] = true;
            }
        }

        flameCounts[2]++;
        if (flameCounts[2] > 50)
        {
            if (flashingFlag[3] == false)
            {
                field.CreateFieldFrame(PixelColors.color_White);
                flashingFlag[3] = true;
                isFlashingDelete = false;
            }
        }
    }

    public void FlashFrameGameOver()
    {
        if (flashingFlag[0] == false)
        {
            field.CreateFieldFrame(PixelColors.color_Blue);
            flashingFlag[0] = true;
        }

        flameCounts[0]++;
        if (flameCounts[0] > 10)
        {
            if (flashingFlag[1] == false)
            {
                field.CreateFieldFrame(PixelColors.color_DarkBlue);
                flashingFlag[1] = true;
            }
        }

        flameCounts[1]++;
        if (flameCounts[1] > 60)
        {
            if (flashingFlag[2] == false)
            {
                field.CreateFieldFrame(PixelColors.color_Black);
                flashingFlag[2] = true;
            }
        }

        flameCounts[2]++;
        if (flameCounts[2] > 110)
        {
            if (flashingFlag[3] == false)
            {
                field.CreateFieldFrame(PixelColors.color_Black);
                flashingFlag[3] = true;
                isFlashingGameOver = false;
            }
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < flameCounts.Length; i++)
        {
            flameCounts[i] = 0;
        }

        for (int i = 0; i < flashingFlag.Length; i++)
        {
            flashingFlag[i] = false;
        }
    }
}
