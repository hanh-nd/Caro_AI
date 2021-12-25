using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;


namespace CaroAI
{
    class SimulationBoard
    {
        #region Properties

        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public int ComputerID = 0;
        public int Times = 0;
        public int XWins = 0;
        public int OWins = 0;
        public int DrawTimes = 0;
        public int LoopTimes = 2;

        public int XMoves = 0;
        public int OMoves = 0;

        public List<int> OTotalMoves;
        public List<int> XTotalMoves;

        private int CountMove;
        private int[,] cells;
        private event EventHandler playerMarked;
        public event EventHandler PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }


        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }
        public AdvancedAI advancedAI;
        public SimpleAI simpleAI;

        #endregion

        #region Initialize
        public SimulationBoard(Panel board, Game form)
        {
            cells = new int[Cons.BOARD_WIDTH, Cons.BOARD_HEIGHT];
            XTotalMoves = new List<int>();
            OTotalMoves = new List<int>();
        }
        #endregion

        #region Methods
        public void DrawBoard()
        {
            Console.WriteLine("Playing...");
            CurrentPlayer = 0;
            XMoves = 0;
            OMoves = 0;
            advancedAI = new AdvancedAI(1);
            simpleAI = new SimpleAI(2);

            // Reset game
            Array.Clear(cells, 0, cells.Length);
            CountMove = 0;      
            Simulation_Click(new Point(new Random().Next(Cons.BOARD_WIDTH / 4, 2 * Cons.BOARD_WIDTH / 3), new Random().Next(Cons.BOARD_WIDTH / 4, 2 * Cons.BOARD_WIDTH / 3)));
        }

        private void Simulation_Click(Point point)
        {
            CountMove++;
            if (CurrentPlayer == 0) XMoves++;
            else OMoves++;
            cells[point.X, point.Y] = CurrentPlayer + 1;
            //PrintCurrentBoard();
            if (isEndGame(point))
            {
                EndGame();
                return;
            }
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            ComputerMove();
        }

        private void ComputerMove()
        {
            Point point;
            if (CurrentPlayer == 0)
                point = advancedAI.FindBestMove(cells, CurrentPlayer + 1);
            else
                point = simpleAI.FindBestMove(cells, CurrentPlayer + 1);           
            Simulation_Click(point);                
        }

        private void PrintCurrentBoard()
        {
            for (int rw = 0; rw < Cons.BOARD_HEIGHT; ++rw)
            {
                for (int cl = 0; cl < Cons.BOARD_WIDTH; ++cl)
                {
                    Console.Write(cells[rw, cl] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("=================");
        }

        public bool Undo()
        {
            return false;
        }

        public void HelpPlayerMove()
        {
        }

        #region check end game
        public void EndGame()
        {
            if (isDraw()) DrawTimes++;
            else
            {
                if (CurrentPlayer == 1)
                {
                    OTotalMoves.Add(OMoves);
                    OWins++;
                }
                else
                {
                    XTotalMoves.Add(XMoves);
                    XWins++;
                }
            }                
            Console.WriteLine(Cons.BOARD_WIDTH + "x" + Cons.BOARD_HEIGHT + ": " + "Advance AI win: " + XWins + " times, Simple AI win: " + OWins + " times, Draw times: " + DrawTimes);
            Times++;
            if (Times >= LoopTimes) 
            {
                double XAverageMoves = XTotalMoves.Sum() * 1.0 / XWins;
                double OAverageMoves = OTotalMoves.Sum() * 1.0 / OWins;

                Console.WriteLine("Average number of Advance AI moves to win: " + XAverageMoves);
                Console.WriteLine("Average number of Simple AI moves to win: " + OAverageMoves);

                Environment.Exit(0);
            }
            DrawBoard();
        }
        private bool isEndGame(Point point)
        {
            return isDraw() || isEndHorizontal(point) || isEndVertical(point) || isEndPrimaryDiagonal(point) || isEndSecondaryDiagonal(point);
        }

        private bool isDraw()
        {
            return CountMove >= Cons.BOARD_HEIGHT * Cons.BOARD_WIDTH;
        }
        private bool isEndHorizontal(Point point)
        {
            int countLeft = 0;
            for (int i = point.X; i >= 0; --i)
            {
                if (cells[i, point.Y] == CurrentPlayer + 1)
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Cons.BOARD_WIDTH; ++i)
            {
                if (cells[i, point.Y] == CurrentPlayer + 1)
                {
                    countRight++;
                }
                else
                    break;
            }

            if (countLeft + countRight >= 5)
            {
                if (point.X - countLeft < 0)
                {
                    if (cells[point.X + countRight + 1, point.Y] == 0)
                        return true;
                    return false;
                }

                if (point.X + countRight + 1 >= Cons.BOARD_WIDTH)
                {
                    if (cells[point.X - countLeft, point.Y] == 0)
                        return true;
                    return false;
                }
                if (cells[point.X - countLeft, point.Y] == 0 || cells[point.X + countRight + 1, point.Y] == 0)
                    return true;
            }

            return false;
        }
        private bool isEndVertical(Point point)
        {
            int countTop = 0;
            for (int i = point.Y; i >= 0; --i)
            {
                if (cells[point.X, i] == CurrentPlayer + 1)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.BOARD_HEIGHT; ++i)
            {
                if (cells[point.X, i] == CurrentPlayer + 1)
                {
                    countBottom++;
                }
                else
                    break;
            }

            if (countTop + countBottom >= 5)
            {
                if (point.Y - countTop < 0)
                {
                    if (cells[point.X, point.Y + countBottom + 1] == 0)
                        return true;
                    return false;
                }

                if (point.Y + countBottom + 1 > Cons.BOARD_HEIGHT)
                {
                    if (cells[point.X, point.Y - countTop] == 0)
                        return true;
                    return false;
                }
                if (cells[point.X, point.Y - countTop] == 0 || cells[point.X, point.Y + countBottom + 1] == 0)
                    return true;
            }
            return false;
        }
        private bool isEndPrimaryDiagonal(Point point)
        {
            int countUp = 0;
            for (int i = 0; i <= point.X; ++i)
            {
                if (point.X - i < 0 || point.Y - i < 0) break;
                if (cells[point.X - i, point.Y - i] == CurrentPlayer + 1)
                {
                    countUp++;
                }
                else
                    break;
            }

            int countDown = 0;
            for (int i = 1; i <= Cons.BOARD_WIDTH - point.X; ++i)
            {
                if (point.X + i >= Cons.BOARD_WIDTH || point.Y + i >= Cons.BOARD_HEIGHT) break;
                if (cells[point.X + i, point.Y + i] == CurrentPlayer + 1)
                {
                    countDown++;
                }
                else
                    break;
            }


            if (countUp + countDown >= 5)
            {
                if (point.X - countUp < 0 || point.Y - countUp < 0)
                {
                    if (cells[point.X + countDown + 1, point.Y + countDown + 1] == 0)
                        return true;
                    return false;
                }

                if (point.X + countDown + 1 >= Cons.BOARD_WIDTH || point.Y + countDown + 1 >= Cons.BOARD_HEIGHT)
                {
                    if (cells[point.X - countUp, point.Y - countUp] == 0)
                        return true;
                    return false;
                }
                if (cells[point.X - countUp, point.Y - countUp] == 0 || cells[point.X + countDown + 1, point.Y + countDown + 1] == 0)
                    return true;
            }

            return false;
        }
        private bool isEndSecondaryDiagonal(Point point)
        {
            int countUp = 0;
            for (int i = 0; i <= point.Y; ++i)
            {
                if (point.X + i >= Cons.BOARD_WIDTH || point.Y - i < 0) break;
                if (cells[point.X + i, point.Y - i] == CurrentPlayer + 1)
                {
                    countUp++;
                }
                else
                    break;
            }

            int countDown = 0;
            for (int i = 1; i <= Cons.BOARD_WIDTH - point.Y; ++i)
            {
                if (point.X - i < 0 || point.Y + i >= Cons.BOARD_HEIGHT) break;
                if (cells[point.X - i, point.Y + i] == CurrentPlayer + 1)
                {
                    countDown++;
                }
                else
                    break;
            }

            if (countUp + countDown >= 5)
            {
                if (point.X - countDown - 1 < 0 || point.Y + countDown + 1 >= Cons.BOARD_HEIGHT)
                {
                    if (cells[point.X + countUp, point.Y - countUp] == 0)
                        return true;
                    return false;
                }

                if (point.X + countUp > Cons.BOARD_WIDTH || point.Y - countUp < 0)
                {
                    if (cells[point.X - countDown - 1, point.Y + countDown + 1] == 0)
                        return true;
                    return false;
                }
                if (cells[point.X - countDown - 1, point.Y + countDown + 1] == 0 || cells[point.X + countUp, point.Y - countUp] == 0)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #endregion
    }
}
