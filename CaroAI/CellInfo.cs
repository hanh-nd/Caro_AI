

using System.Drawing;

namespace CaroAI
{
    class CellInfo
    {
        private Point point;
        public Point Point { get => point; set => point = value; }

        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public CellInfo(Point point, int currentPlayer)
        {
            this.Point = point;
            this.CurrentPlayer = currentPlayer;
        }
    }
}
