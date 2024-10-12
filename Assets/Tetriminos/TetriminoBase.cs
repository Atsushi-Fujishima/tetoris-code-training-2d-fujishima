using System;

namespace TetrimimoType
{
    public class TetriminoBase
    {
        private bool isConfirm = false; //trueなら動かせない
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

        public bool IsConfirm
        {
            get { return isConfirm; }
        }

        public virtual void DrawTetrimino(IMachine machine, (int, int) startGridSquarePosition)
        {

        }

        public void Move((int, int) vector)
        {
            if (IsMove())
            {
                var currentPositions = new (int, int)[blocks.Length];
                var nextPositions = new (int, int)[blocks.Length];

                for (int i = 0; i < blocks.Length; i++)
                {
                    currentPositions[i] = blocks[i].GetGridSquarePosition();
                    nextPositions[i] = new(currentPositions[i].Item1 + vector.Item1, currentPositions[i].Item2 + vector.Item2);
                }

                foreach (var block in blocks)
                {
                    block.EraseBlock();
                    block.GetCurrentGridSquare().IsInBlock = false;
                }

                // move
                for (int i = 0; i < blocks.Length; i++)
                {
                    blocks[i].DrawBlock(nextPositions[i]);
                    blocks[i].GetCurrentGridSquare().IsInBlock = true;
                }
            }
            else
            {
                isConfirm = true;
                return;
            }
        }

        public void Rotate()
        {
            (int, int)[] rotationPositions = new (int, int)[blocks.Length];
            (int, int) xRange = new(100, 0); // (min , max)
            int yMax = 0;
            
            foreach (var block in blocks)
            {
                block.EraseBlock();
                block.GetCurrentGridSquare().IsInBlock = false;
            }

            for (int i = 0; i < blocks.Length; i++)
            {
                var centerPos = blocks[0].GetGridSquarePosition();
                var blockPos = blocks[i].GetGridSquarePosition();

                UnityEngine.Debug.Log("pos" + blockPos.Item1 + ", " + blockPos.Item2);

                // 相対座標
                int relativeX = blockPos.Item1 - centerPos.Item1;
                int relativeY = blockPos.Item2 - centerPos.Item2;

                // 90度の座標変換
                int xr = -relativeY;
                int yr = relativeX;

                // 座標を戻す
                xr += centerPos.Item1;
                yr += centerPos.Item2;

                rotationPositions[i] = (xr, yr);

                // フィールドに収まっているかの確認用
                xRange.Item1 = Math.Min(rotationPositions[i].Item1, xRange.Item1);
                xRange.Item2 = Math.Max(rotationPositions[i].Item1, xRange.Item2);
                yMax = Math.Max(rotationPositions[i].Item2, yMax);
            }

            for (int i = 0; i < blocks.Length; i++)
            {
                if (xRange.Item1 < FieldGridSquareList.instance.rowsRange.Item1)
                {
                    rotationPositions[i].Item1 += 1;
                }

                if (xRange.Item2 > FieldGridSquareList.instance.rowsRange.Item2)
                {
                    rotationPositions[i].Item1 -= 1;
                }

                if (yMax > FieldGridSquareList.instance.columnsRange.Item2)
                {
                    rotationPositions[i].Item2 -= 1;
                }

                blocks[i].DrawBlock(rotationPositions[i]);
                blocks[i].GetCurrentGridSquare().IsInBlock = true;
            }
        }

        private bool IsMove()
        {
            int downAmount = 1;
            bool isDuplicate = false;
            var currentPositions = new (int, int)[blocks.Length];
            var downPositions = new (int, int)[blocks.Length];
            FieldGridSquareList gridSquareList = FieldGridSquareList.instance;

            for (int i = 0; i < blocks.Length; i++)
            {
                currentPositions[i] = blocks[i].GetGridSquarePosition();
                downPositions[i] = (currentPositions[i].Item1, currentPositions[i].Item2 + downAmount);
            }

            foreach (var dPos in downPositions)
            {
                // null check
                FieldGridSquare gs = gridSquareList.GetGridSquarePositionOf(dPos);
                if (gs == null)
                {
                    UnityEngine.Debug.Log("a");
                    return false;
                }

                // field range check
                if (gs.GetGridSquarePosition().Item2 == 20)
                {
                    UnityEngine.Debug.Log("b");
                    UnityEngine.Debug.Log(gs.GetGridSquarePosition());
                    return false;
                    
                }
                else
                {
                    UnityEngine.Debug.Log(gs.GetGridSquarePosition());
                }

                // duplicate check in currentPositions
                foreach (var cPos in currentPositions)
                {
                    if (dPos == cPos)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (isDuplicate == false && gs.IsInBlock)
                {
                    UnityEngine.Debug.Log("c");
                    return false;
                }
            }

            return true;
        }
    }
}