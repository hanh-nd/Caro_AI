using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaroAI
{
    class Utility
    {
        private int ComputerTurn, PlayerTurn;
        public Utility(int ComputerTurn)
        {
            this.ComputerTurn = ComputerTurn;
            this.PlayerTurn = ComputerTurn == 1 ? 2 : 1;
        }
        public bool isEndGame(int[,] CurrentStatus, Point point, bool ForPlayer)
        {
            return isEndHorizontal(CurrentStatus, point, ForPlayer) || isEndVertical(CurrentStatus, point, ForPlayer) 
                || isEndPrimaryDiagonal(CurrentStatus, point, ForPlayer) || isEndSecondaryDiagonal(CurrentStatus, point, ForPlayer);
        }

        private bool isEndHorizontal(int[,] CurrentStatus, Point point, bool ForPlayer)
        {
            int countLeft = 0;
            for (int i = point.X; i >= 0; --i)
            {
                if (CurrentStatus[i, point.Y] == (ForPlayer ? PlayerTurn : ComputerTurn))
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Cons.BOARD_WIDTH; ++i)
            {
                if (CurrentStatus[i, point.Y] == (ForPlayer ? PlayerTurn : ComputerTurn))
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
                    if (CurrentStatus[point.X + countRight + 1, point.Y] == 0)
                        return true;
                    return false;
                }

                if (point.X + countRight + 1 >= Cons.BOARD_WIDTH)
                {
                    if (CurrentStatus[point.X - countLeft, point.Y] == 0)
                        return true;
                    return false;
                }
                if (CurrentStatus[point.X - countLeft, point.Y] == 0 || CurrentStatus[point.X + countRight + 1, point.Y] == 0)
                    return true;
            }

            return false;
        }
        private bool isEndVertical(int[,] CurrentStatus, Point point, bool ForPlayer)
        {
            int countTop = 0;
            for (int i = point.Y; i >= 0; --i)
            {
                if (CurrentStatus[point.X, i] == (ForPlayer ? PlayerTurn : ComputerTurn))
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.BOARD_HEIGHT; ++i)
            {
                if (CurrentStatus[point.X, i] == (ForPlayer ? PlayerTurn : ComputerTurn))
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
                    if (CurrentStatus[point.X, point.Y + countBottom + 1] == 0)
                        return true;
                    return false;
                }

                if (point.Y + countBottom + 1 >= Cons.BOARD_HEIGHT)
                {
                    if (CurrentStatus[point.X, point.Y - countTop] == 0)
                        return true;
                    return false;
                }
                if (CurrentStatus[point.X, point.Y - countTop] == 0 || CurrentStatus[point.X, point.Y + countBottom + 1] == 0)
                    return true;
            }
            return false;
        }
        private bool isEndPrimaryDiagonal(int[,] CurrentStatus, Point point, bool ForPlayer)
        {
            int countUp = 0;
            for (int i = 0; i <= point.X; ++i)
            {
                if (point.X - i < 0 || point.Y - i < 0) break;
                if (CurrentStatus[point.X - i, point.Y - i] == (ForPlayer ? PlayerTurn : ComputerTurn))
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
                if (CurrentStatus[point.X + i, point.Y + i] == (ForPlayer ? PlayerTurn : ComputerTurn))
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
                    if (CurrentStatus[point.X + countDown + 1, point.Y + countDown + 1] == 0)
                        return true;
                    return false;
                }

                if (point.X + countDown + 1 >= Cons.BOARD_WIDTH || point.Y + countDown + 1 >= Cons.BOARD_HEIGHT)
                {
                    if (CurrentStatus[point.X - countUp, point.Y - countUp] == 0)
                        return true;
                    return false;
                }
                if (CurrentStatus[point.X - countUp, point.Y - countUp] == 0 || CurrentStatus[point.X + countDown + 1, point.Y + countDown + 1] == 0)
                    return true;
            }

            return false;
        }
        private bool isEndSecondaryDiagonal(int[,] CurrentStatus, Point point, bool ForPlayer)
        {
            int countUp = 0;
            for (int i = 0; i <= point.Y; ++i)
            {
                if (point.X + i >= Cons.BOARD_WIDTH || point.Y - i < 0) break;
                if (CurrentStatus[point.X + i, point.Y - i] == (ForPlayer ? PlayerTurn : ComputerTurn))
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
                if (CurrentStatus[point.X - i, point.Y + i] == (ForPlayer ? PlayerTurn : ComputerTurn))
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
                    if (CurrentStatus[point.X + countUp, point.Y - countUp] == 0)
                        return true;
                    return false;
                }

                if (point.X + countUp >= Cons.BOARD_WIDTH || point.Y - countUp < 0)
                {
                    if (CurrentStatus[point.X - countDown - 1, point.Y + countDown + 1] == 0)
                        return true;
                    return false;
                }
                if (CurrentStatus[point.X - countDown - 1, point.Y + countDown + 1] == 0 || CurrentStatus[point.X + countUp, point.Y - countUp] == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
