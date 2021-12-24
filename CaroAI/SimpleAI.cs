using System.Collections.Generic;
using System.Drawing;
using System;

namespace CaroAI
{

    class SimpleAI : AI
    {
        public SimpleAI(int ComputerTurn): base(ComputerTurn) { }
        public override List<Point> GetPossibleMoves(int[,] CurrentStatus, int CurrentTurn)
        {
            List<Point> Moves = new List<Point>();
            for (int rw = 0; rw < Cons.BOARD_HEIGHT; rw++)
            {
                for (int cl = 0; cl < Cons.BOARD_WIDTH; cl++)
                {
                    if (IsPossibleMove(CurrentStatus, rw, cl))
                    {
                        Moves.Add(new Point(rw, cl));
                    }
                }
            }
            return Moves;
        }

        private bool IsPossibleMove(int [,] CurrentStatus, int i, int j)
        {
			if (CurrentStatus[i, j] > 0) return false;

			for (int x = -1; x <= 1; x++)
				for (int y = -1; y <= 1; y++)
					if (CheckPosition(i + x, j + y) && CurrentStatus[i + x, j + y] > 0)
						return true;
			return false;
		}
    }
}


