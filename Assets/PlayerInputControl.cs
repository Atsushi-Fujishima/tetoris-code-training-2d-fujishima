
public class PlayerInputControl
{
    private IMachine machine;

    private bool isLeftKeySet = false;
    private int leftKeyCount = 0;

    private bool isRightKeySet = false;
    private int rightKeyCount = 0;

    private bool isDownKeySet = false;
    private int downKeyCount = 0;

    private bool isUpKeySet = false;

    public PlayerInputControl(IMachine machine)
    {
        this.machine = machine;
    }

    // ���������Ă���
    public bool LeftKey()
    {
        if (machine.Left)
        {
            if (leftKeyCount > ConstList.KEYPRESSHOLDCOUNT)
            {
                return true;
            }
            else
            {
                leftKeyCount++;
                return false;
            }
        }
        else
        {
            leftKeyCount = 0;
            return false;
        }
    }

    public bool RightKey()
    {
        if (machine.Right)
        {
            if (rightKeyCount > ConstList.KEYPRESSHOLDCOUNT)
            {
                return true;
            }
            else
            {
                rightKeyCount++;
                return false;
            }
        }
        else
        {
            rightKeyCount = 0;
            return false;
        }
    }

    public bool DownKey()
    {
        if (machine.Down)
        {
            if (downKeyCount > ConstList.KEYPRESSHOLDCOUNT)
            {
                return true;
            }
            else
            {
                downKeyCount++;
                return false;
            }
        }
        else
        {
            downKeyCount = 0;
            return false;
        }
    }

    // �ۂ����Ɖ���
    public bool LeftKeyWasPressed()
    {
        if (machine.Left)
        {
            // ��������
            if (isLeftKeySet)
            {
                return false;
            }

            isLeftKeySet = true;
            return true;
        }
        else
        {
            // �����ĂȂ�
            isLeftKeySet = false;
            return false;
        }
    }

    public bool RightKeyWasPressed()
    {
        if (machine.Right)
        {
            if (isRightKeySet)
            {
                return false;
            }

            isRightKeySet = true;
            return true;
        }
        else
        {
            isRightKeySet = false;
            return false;
        }
    }

    public bool DownKeyWasPressed()
    {
        if (machine.Down)
        {
            if (isDownKeySet)
            {
                return false;
            }

            isDownKeySet = true;
            return true;
        }
        else
        {
            isDownKeySet = false;
            return false;
        }
    }

    public bool UpKeyWasPressed()
    {
        if (machine.Up)
        {
            if (isUpKeySet)
            {
                return false;
            }

            isUpKeySet = true;
            return true;
        }
        else
        {
            isUpKeySet = false;
            return false;
        }
    }
}
