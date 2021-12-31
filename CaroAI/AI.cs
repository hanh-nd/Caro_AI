using System;
using System.Collections.Generic;
using System.Drawing;

namespace CaroAI
{
    abstract class AI
    {
        private int[] points = { 1, 3, 5, 7, 10, 200, 50000, 250000, 1000000 };
        protected const int WINNING_POINT = 100000000;
        protected int ComputerTurn, PlayerTurn;
        protected Random rand = new Random();
        private Utility utility;
        public AI(int ComputerTurn)
        {
            this.ComputerTurn = ComputerTurn;
            this.PlayerTurn = ComputerTurn == 1 ? 2 : 1;
            utility = new Utility(ComputerTurn);
        }

        public Point FindBestMove(int[,] CurrentStatus, int CurrentTurn)
        {           
            List<Point> Moves = GetPossibleMoves(CurrentStatus, CurrentTurn);
            Console.WriteLine("Founded: " + Moves.Count + " moves");
            var move = SearchWinningMove(CurrentStatus, Moves, false);
            if (move.X != -1) return move;
            move = SearchWinningMove(CurrentStatus, Moves, true);
            if (move.X != -1) return move; 

            double[] FeasiblePointValues = new double[Moves.Count];

            double alpha = -1, beta = WINNING_POINT;
            int depth = 0;
            double bestValue = -1;

            for (int i = 0; i < Moves.Count; ++i)
            {
                CurrentStatus[Moves[i].X, Moves[i].Y] = ComputerTurn;
                double v = MinValue(CurrentStatus, alpha, beta, depth);
                CurrentStatus[Moves[i].X, Moves[i].Y] = 0;
                FeasiblePointValues[i] = v;
                if (v > bestValue) bestValue = v;
            }

            List<Point> list = new List<Point>();

            for (int i = 0; i < FeasiblePointValues.Length; i++)
            {
                if (bestValue <= FeasiblePointValues[i])
                {
                    list.Add(new Point(Moves[i].X, Moves[i].Y));
                }
            }

            int x = rand.Next(0, list.Count);
            //foreach (Point m in list)
            //{
            //    Console.WriteLine("list: " + m);
            //}
            //Console.WriteLine("choose: " + list[x]);
            return new Point(list[x].X, list[x].Y);
        }

        private Point SearchWinningMove(int[,] CurrentStatus, List<Point> moves, bool ForPlayer)
        {
            foreach (Point move in moves)
            {
                CurrentStatus[move.X, move.Y] = (ForPlayer ? PlayerTurn : ComputerTurn);
                if (utility.isEndGame(CurrentStatus, move, ForPlayer))
                {
                    return move;
                }
                CurrentStatus[move.X, move.Y] = 0;
            }
            return new Point(-1, -1);
        }

        public abstract List<Point> GetPossibleMoves(int[,] CurrentStatus, int CurrentTurn);

        public bool CheckPosition(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < Cons.BOARD_HEIGHT && y < Cons.BOARD_WIDTH);
        }
        public double MaxValue(int[,] CurrentStatus, double alpha, double beta, int depth)
        {
            List<Point> Moves = GetPossibleMoves(CurrentStatus, ComputerTurn);
            double value = Utility(CurrentStatus, false);
            if (depth >= Cons.MAX_DEPTH || Moves.Count == 0)
                return value;

            foreach (Point move in Moves)
            {
                CurrentStatus[move.X, move.Y] = ComputerTurn;
                value = Math.Max(value, MinValue(CurrentStatus, alpha, beta, depth + 1));
                if (value >= beta)
                {
                    CurrentStatus[move.X, move.Y] = 0;
                    return value;
                }
                CurrentStatus[move.X, move.Y] = 0;
                alpha = Math.Max(alpha, value);
            }
            return value;
        }

        public double MinValue(int[,] CurrentStatus, double alpha, double beta, int depth)
        {
            List<Point> Moves = GetPossibleMoves(CurrentStatus, PlayerTurn);
            double value = Utility(CurrentStatus, true);
            if (depth >=  Cons.MAX_DEPTH || Moves.Count == 0)
                return value;

            foreach (Point move in Moves)
            {
                CurrentStatus[move.X, move.Y] = PlayerTurn; 
                value = Math.Min(value, MaxValue(CurrentStatus, alpha, beta, depth + 1));
                if (alpha >= value)
                {
                    CurrentStatus[move.X, move.Y] = 0;
                    return value;
                }

                CurrentStatus[move.X, move.Y] = 0;
                beta = Math.Min(beta, value);
            }
            return value;

        }

        /* Utility
         * @param CurrentStatus
         * @param PlayerTurn
         * return the final state value of a move
         */
        public double Utility(int[,] CurrentStatus, bool PlayerTurn)
        {
            int PlayerScore = GetScore(CurrentStatus, true, PlayerTurn);
            int ComputerScore = GetScore(CurrentStatus, false, PlayerTurn);
            if (PlayerScore == 0) PlayerScore = 1;
            return ComputerScore * 1.0 / PlayerScore * 1.0;
        }
        public int GetScore(int[,] CurrentStatus, bool ForPlayer, bool PlayerTurn)
        {
            int h = evaluateHorizontal(CurrentStatus, ForPlayer, PlayerTurn);
            int v = evaluateVertical(CurrentStatus, ForPlayer, PlayerTurn);
            int pD = evaluatePrimaryDiagonal(CurrentStatus, ForPlayer, PlayerTurn);
            int sD = evaluateSecondaryDiagonal(CurrentStatus, ForPlayer, PlayerTurn);
            return h + v + pD + sD;
        }

        private int evaluateHorizontal(int[,] CurrentStatus, bool ForPlayer, bool IsPlayerTurn)
        {
            int consecutive = 0;
            int blocks = 2;
            int score = 0;

            for (int rw = 0; rw < Cons.BOARD_HEIGHT; ++rw)
            {
                for (int cl = 0; cl < Cons.BOARD_WIDTH; ++cl)
                {
                    if (CurrentStatus[rw, cl] == (ForPlayer ? PlayerTurn : ComputerTurn)) 
                        consecutive++;
                    else if (CurrentStatus[rw, cl] == 0)
                    {
                        for (int i = 1; i <= 5; ++i)
                        {
                            if (CheckPosition(rw, cl + i) && CurrentStatus[rw, cl + i] == (ForPlayer ? PlayerTurn : ComputerTurn))
                            {
                                consecutive++;
                            }
                            else if (CheckPosition(rw, cl + i) && CurrentStatus[rw, cl + i] == 0)
                            {
                                blocks--;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (consecutive > 0)
                        {                        
                            score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                            consecutive = 0;
                        }
                        blocks = 1;
                    }
                    else if (consecutive > 0)
                    {
                        score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                        consecutive = 0;
                        blocks = 2;
                    }
                    else
                        blocks = 2;
                }
                if (consecutive > 0)
                {
                    score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                }
                consecutive = 0;
                blocks = 2;
            }

            return score;
        }
        private int evaluateVertical(int[,] CurrentStatus, bool ForPlayer, bool IsPlayerTurn)
        {
            int consecutive = 0;
            int blocks = 2;
            int score = 0;

            for (int cl = 0; cl < Cons.BOARD_WIDTH; ++cl)
            {
                for (int rw = 0; rw < Cons.BOARD_WIDTH; ++rw)
                {
                    if (CurrentStatus[rw, cl] == (ForPlayer ? PlayerTurn : ComputerTurn))
                    {
                        consecutive++;
                    }
                    else if (CurrentStatus[rw, cl] == 0)
                    {
                        for (int i = 1; i <= 5; ++i)
                        {
                            if (CheckPosition(rw + i, cl) && CurrentStatus[rw + i, cl] == (ForPlayer ? PlayerTurn : ComputerTurn))
                            {
                                consecutive++;
                            }
                            else if (CheckPosition(rw + i, cl) && CurrentStatus[rw + i, cl] == 0)
                            {
                                blocks--;
                                break;
                            }
                            else break;
                        }
                        if (consecutive > 0)
                        {
                            score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                            consecutive = 0;
                        }
                        blocks = 1;
                    }
                    else if (consecutive > 0)
                    {
                        score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                        consecutive = 0;
                        blocks = 2;
                    }
                    else
                    {
                        blocks = 2;
                    }
                }

                if (consecutive > 0)
                {
                    score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                }

                consecutive = 0;
                blocks = 2;
            }
             
            return score;
        }
        private int evaluatePrimaryDiagonal(int[,] CurrentStatus, bool ForPlayer, bool IsPlayerTurn)
        {
            int consecutive = 0;
            int blocks = 2;
            int score = 0;

            for (int k = 1 - Cons.BOARD_HEIGHT; k < Cons.BOARD_HEIGHT; ++k)
            {
                int iStart = Math.Max(0, k);
                int iEnd = Math.Min(Cons.BOARD_HEIGHT + k - 1, Cons.BOARD_HEIGHT - 1);
                for (int i = iStart; i <= iEnd; ++i)
                {
                    int j = i - k;

                    if (CurrentStatus[i, j] == (ForPlayer ? PlayerTurn : ComputerTurn))
                    {
                        consecutive++;
                    }
                    else if (CurrentStatus[i, j] == 0)
                    {
                        for (int rw = 1; rw <= 5; ++rw)
                        {
                            if (CheckPosition(i + rw, j + rw) && CurrentStatus[i + rw, j + rw] == (ForPlayer ? PlayerTurn : ComputerTurn))
                            {
                                consecutive++;
                            }
                            else if (CheckPosition(i + rw, j + rw) && CurrentStatus[i + rw, j + rw] == 0)
                            {
                                blocks--;
                                break;
                            }
                            else break;
                        }
                        if (consecutive > 0)
                        {
                            score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                            consecutive = 0;
                        }
                        blocks = 1;
                    }
                    else if (consecutive > 0)
                    {
                        score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                        consecutive = 0;
                        blocks = 2;
                    }
                    else
                    {
                        blocks = 2;
                    }
                }
                if (consecutive > 0)
                {
                    score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                }
                consecutive = 0;
                blocks = 2;
            }
            return score;
        }
        private int evaluateSecondaryDiagonal(int[,] CurrentStatus, bool ForPlayer, bool IsPlayerTurn)
        {
            int consecutive = 0;
            int blocks = 2;
            int score = 0;

            for (int k = 0; k <= 2 * (Cons.BOARD_HEIGHT - 1); ++k)
            {
                int iStart = Math.Max(0, k - Cons.BOARD_HEIGHT + 1);
                int iEnd = Math.Min(Cons.BOARD_HEIGHT - 1, k);
                for (int i = iStart; i <= iEnd; ++i)
                {
                    int j = k - i;

                    if (CurrentStatus[i, j] == (ForPlayer ? PlayerTurn : ComputerTurn))
                    {
                        consecutive++;
                    }
                    else if (CurrentStatus[i, j] == 0)
                    {
                        for (int rw = 1; rw <= 5; ++rw)
                        {
                            if (CheckPosition(i + rw, j - rw) && CurrentStatus[i + rw, j - rw] == (ForPlayer ? PlayerTurn : ComputerTurn))
                            {
                                consecutive++;
                            }
                            else if (CheckPosition(i + rw, j - rw) && CurrentStatus[i + rw, j - rw] == 0)
                            {
                                blocks--;
                                break;
                            }
                            else break;
                        }
                        if (consecutive > 0)
                        {
                            score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                            consecutive = 0;
                        }
                        blocks = 1;
                    }
                    else if (consecutive > 0)
                    {
                        score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                        consecutive = 0;
                        blocks = 2;
                    }
                    else
                    {
                        blocks = 2;
                    }
                }
                if (consecutive > 0)
                {
                    score += getConsecutiveSetScore(consecutive, blocks, ForPlayer == IsPlayerTurn);
                }
                consecutive = 0;
                blocks = 2;
            }
            return score;
        }

        // This function returns the score of a given consecutive stone set.
        // count: Number of consecutive stones in the set
        // blocks: Number of blocked sides of the set (2: both sides blocked, 1: single side blocked, 0: both sides free)
        private int getConsecutiveSetScore(int count, int blocks, bool CurrentTurn)
        {
            if (blocks == 2 && count <= 5) return 0;
            switch (count)
            {            
                case 5:
                    return WINNING_POINT;

                case 4:
                    if (CurrentTurn) return points[8];
                    else
                    {
                        if (blocks == 0) return points[7];
                        else return points[5];
                    }
                case 3:
                    if (blocks == 0)
                    {
                        if (CurrentTurn) return points[6];
                        else return points[5];
                    }
                    else
                    {
                        if (CurrentTurn) return points[4];
                        else return points[2];
                    }

                case 2:
                    if (blocks == 0)
                    {
                        if (CurrentTurn) return points[3];
                        else return points[2];
                    }
                    else
                    {
                        return points[1];
                    }

                case 1:
                    return points[0];
                case 0:
                    return 0;
            }
            return WINNING_POINT * 2;
        }
    }
}
