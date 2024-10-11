
namespace TetrimimoType
{
    public class TetriminoBase
    {
        protected byte[] color;
        protected Block[] blocks = new Block[ConstList.TETRIMINOOFBLOCKNUM];

        public byte[] Color
        {
            set { color = value; }
            get { return color; }
        }

        public Block[] Blocks
        {
            get { return blocks; }
        }

        public virtual void DrawTetrimino(IMachine machine, FieldGridSquareList gridSquareList, (int, int) startGridSquarePosition)
        {

        }
    }
}