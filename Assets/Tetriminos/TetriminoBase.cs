
using UnityEngine;

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

        public virtual void DrawTetrimino(IMachine machine, (int, int) startGridSquarePosition)
        {

        }

        public void Move((int, int) vector)
        {
            foreach (var block in this.blocks)
            {
                block.EraseBlock();
            }

            foreach (var block in blocks)
            {
                (int, int) nextPosition = (block.GetGridSquarePosition().Item1 + vector.Item1, block.GetGridSquarePosition().Item2 + vector.Item2);
                block.DrawBlock(nextPosition);
            }
        }
    }
}