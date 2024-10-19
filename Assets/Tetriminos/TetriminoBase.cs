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
            set {  isConfirm = value; }
            get { return isConfirm; }
        }

        public virtual void DrawTetrimino(IMachine machine, (int, int) startGridSquarePosition)
        {

        }

        public void Move((int, int) vector)
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
            }

            // move
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].DrawBlock(nextPositions[i]);
            }
        }

        public void Rotate()
        {
            var currentPositions = new (int, int)[blocks.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                currentPositions[i] = blocks[i].GetGridSquarePosition();
            }

            (int, int)[] rotationPositions = new (int, int)[blocks.Length];
            (int, int) xRange = new(100, 0); // (min , max)
            int yMax = 0;
            
            for (int i = 0; i < blocks.Length; i++)
            {
                var centerPos = blocks[0].GetGridSquarePosition();
                var blockPos = blocks[i].GetGridSquarePosition();

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
                int xRowsMin = FieldGridSquareList.instance.rowsRange.Item1;
                int xRowsMax = FieldGridSquareList.instance.rowsRange.Item2;
                int yColumnsMax = FieldGridSquareList.instance.columnsRange.Item2;

                if (xRange.Item1 < xRowsMin)
                {
                    rotationPositions[i].Item1 += xRowsMin - xRange.Item1;
                }

                if (xRange.Item2 > xRowsMax)
                {
                    rotationPositions[i].Item1 += xRowsMax - xRange.Item2;
                }

                if (yMax > yColumnsMax)
                {
                    rotationPositions[i].Item2 -= yColumnsMax - yMax;
                }
            }

            if (IsDuplicate(currentPositions, rotationPositions, FieldGridSquareList.instance))
            {
                return;
            }

            foreach (var block in blocks)
            {
                block.EraseBlock();
            }

            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].DrawBlock(rotationPositions[i]);
            }
        }

        public bool IsMoveHorizontal(int value)
        {
            FieldGridSquareList gridSquareList = FieldGridSquareList.instance;
            var currentPositions = new (int, int)[blocks.Length];
            var sidePositions = new (int, int)[blocks.Length];

            for (int i = 0; i < blocks.Length; i++)
            {
                currentPositions[i] = blocks[i].GetGridSquarePosition();
                sidePositions[i] = (currentPositions[i].Item1 + value, currentPositions[i].Item2);
            }

            foreach (var block in blocks)
            {
                if (value < 0)
                {
                    if (block.GetCurrentGridSquare().GetGridSquarePosition().Item1 <= gridSquareList.rowsRange.Item1)
                    {
                        return false;
                    }
                }
                else
                {
                    if (block.GetCurrentGridSquare().GetGridSquarePosition().Item1 >= gridSquareList.rowsRange.Item2)
                    {
                        return false;
                    }
                }
            }

            if (IsDuplicate(currentPositions, sidePositions, gridSquareList))
            {
                return false;
            }

            return true;
        }

        public bool IsMoveDown()
        {
            int downAmount = 1;
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
                FieldGridSquare gs = gridSquareList.GetGridSquarePositionOf(dPos);

                if (gs == null)
                {
                    return false;
                }

                if (gs.GetGridSquarePosition().Item2 == ConstList.FIELDVERTICAL)
                {
                    return false;
                }
            }

            if (IsDuplicate(currentPositions, downPositions, gridSquareList))
            {
                return false;
            }

            return true;
        }

        private bool IsDuplicate((int, int)[] currentPositions, (int, int)[] afterPositions, FieldGridSquareList gridList)
        {
            foreach (var aPos in afterPositions)
            {
                bool isDuplicateInTetrimino = false;
                var grid = gridList.GetGridSquarePositionOf(aPos);

                foreach (var cPos in currentPositions)
                {
                    if (aPos == cPos)
                    {
                        isDuplicateInTetrimino = true;
                        break;
                    }
                }

                if (grid != null)
                {
                    if (isDuplicateInTetrimino == false && grid.InBlock != null)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}