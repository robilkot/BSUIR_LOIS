namespace LogicalExpressionClassLibrary.Minimization.Karnaugh
{
    public class TorusTable(bool[][][] table)
    {
        private readonly bool[][][] _array = table;
        private readonly int _arrayDepth = table.Length;
        private readonly int _arrayHeight = table[0].Length;
        private readonly int _arrayWidth = table[0][0].Length;

        public int Depth => _arrayDepth;
        public int Width => _arrayWidth;
        public int Height => _arrayHeight;
        public bool At((int k, int i, int j) point) => At(point.k, point.i, point.j);
        public bool At(int k, int i, int j)
        {
            (int kCoord, int iCoord, int jCoord) = Normalize((k, i, j));
            return _array[kCoord][iCoord][jCoord];
        }

        public (int, int, int) Normalize((int k, int i, int j) point)
        {
            (point.i, point.j) = Normalize2D((point.i, point.j));
            if (Math.Abs(point.k) >= _arrayDepth)
                point.k = point.k - (point.k / _arrayDepth) * _arrayDepth;
            if (point.k < 0)
                point.k = _arrayDepth + point.k;
            return (point.k, point.i, point.j);
        }

        private (int, int) Normalize2D((int i, int j) point)
        {
            if (Math.Abs(point.i) >= _arrayHeight)
                point.i -= (point.i / _arrayHeight) * _arrayHeight;
            if (point.i < 0)
                point.i = _arrayHeight + point.i;

            if (Math.Abs(point.j) >= _arrayWidth)
                point.j -= (point.j / _arrayWidth) * _arrayWidth;
            if (point.j < 0)
                point.j = _arrayWidth + point.j;

            return (point.i, point.j);
        }
    }
}


