using System.Collections.Generic;
using System.Drawing;
using System;

namespace CaroAI
{
    class AdvancedAI: AI
    {
        private int[] AttackPoints = new int[] { 0, 4, 28, 256, 2308 };
        private int[] DefensePoints = new int[] { 0, 1, 9, 85, 769 };
        public AdvancedAI(int ComputerTurn) : base(ComputerTurn) { }

        public override List<Point> GetPossibleMoves(int[,] CurrentStatus, int CurrentTurn)
        {
            int[,] aValues = new int[Cons.BOARD_HEIGHT, Cons.BOARD_WIDTH];

            int n = Cons.BOARD_HEIGHT;
            int cComputer, cPlayer;
            int i, rw, cl;

            // Horizontal
            for (rw = 0; rw < n; rw++)
            {
                for (cl = 0; cl < n - 4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (CurrentStatus[rw, cl + i] == ComputerTurn) 
                            cComputer++;
                        if (CurrentStatus[rw, cl + i] == PlayerTurn) 
                            cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                        {
                            if (CurrentStatus[rw, cl + i] == 0)
                            {
                                if (cComputer == 0)
                                {
                                    if (CurrentTurn == ComputerTurn) 
                                        aValues[rw, cl + i] += DefensePoints[cPlayer];
                                    else aValues[rw, cl + i] += AttackPoints[cPlayer];
                                    if (CheckPosition(rw, cl - 1) && CheckPosition(rw, cl + 5) && CurrentStatus[rw, cl - 1] == ComputerTurn 
                                        && CurrentStatus[rw, cl + 5] == ComputerTurn)
                                        aValues[rw, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (CurrentTurn == PlayerTurn) 
                                        aValues[rw, cl + i] += DefensePoints[cComputer];
                                    else aValues[rw, cl + i] += AttackPoints[cComputer];
                                    if (CheckPosition(rw, cl - 1) && CheckPosition(rw, cl + 5) && CurrentStatus[rw, cl - 1] == PlayerTurn 
                                        && CurrentStatus[rw, cl + 5] == PlayerTurn)
                                        aValues[rw, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((CheckPosition(rw, cl + i - 1) && CurrentStatus[rw, cl + i - 1] == 0) 
                                    || (CheckPosition(rw, cl + i + 1) && CurrentStatus[rw, cl + i + 1] == 0)))
                                    aValues[rw, cl + i] *= 2;
                                else if (cComputer == 4 || cPlayer == 4)
                                    aValues[rw, cl + i] *= 2;
                            }
                        }
                            
                }
            }
                
            // Vertical
            for (rw = 0; rw < n - 4; rw++)
            {
                for (cl = 0; cl < n; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (CurrentStatus[rw + i, cl] == ComputerTurn) 
                            cComputer++;
                        if (CurrentStatus[rw + i, cl] == PlayerTurn) 
                            cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                        {
                            if (CurrentStatus[rw + i, cl] == 0)
                            {
                                if (cComputer == 0)
                                {
                                    if (CurrentTurn == ComputerTurn) 
                                        aValues[rw + i, cl] += DefensePoints[cPlayer];
                                    else aValues[rw + i, cl] += AttackPoints[cPlayer];
                                    if (CheckPosition(rw - 1, cl) && CheckPosition(rw + 5, cl) && CurrentStatus[rw - 1, cl] == ComputerTurn 
                                        && CurrentStatus[rw + 5, cl] == ComputerTurn)
                                        aValues[rw + i, cl] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (CurrentTurn == PlayerTurn) 
                                        aValues[rw + i, cl] += DefensePoints[cComputer];
                                    else aValues[rw + i, cl] += AttackPoints[cComputer];
                                    if (CheckPosition(rw - 1, cl) && CheckPosition(rw + 5, cl) && CurrentStatus[rw - 1, cl] == PlayerTurn 
                                        && CurrentStatus[rw + 5, cl] == PlayerTurn)
                                        aValues[rw + i, cl] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((CheckPosition(rw + i - 1, cl) && CurrentStatus[rw + i - 1, cl] == 0) 
                                    || (CheckPosition(rw + i + 1, cl) && CurrentStatus[rw + i + 1, cl] == 0)))
                                    aValues[rw + i, cl] *= 2;
                                else if (cComputer == 4 || cPlayer == 4)
                                    aValues[rw + i, cl] *= 2;
                            }
                        }                        
                }
            }
                
            // Primary Diagonal 
            for (rw = 0; rw < n - 4; rw++)
            {
                for (cl = 0; cl < n - 4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (CurrentStatus[rw + i, cl + i] == ComputerTurn) 
                            cComputer++;
                        if (CurrentStatus[rw + i, cl + i] == PlayerTurn) 
                            cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                        {
                            if (CurrentStatus[rw + i, cl + i] == 0)
                            {
                                if (cComputer == 0)
                                {
                                    if (CurrentTurn == ComputerTurn) 
                                        aValues[rw + i, cl + i] += DefensePoints[cPlayer];
                                    else aValues[rw + i, cl + i] += AttackPoints[cPlayer];
                                    if (CheckPosition(rw - 1, cl - 1) && CheckPosition(rw + 5, cl + 5) && CurrentStatus[rw - 1, cl - 1] == ComputerTurn 
                                        && CurrentStatus[rw + 5, cl + 5] == ComputerTurn)
                                        aValues[rw + i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (CurrentTurn == PlayerTurn) 
                                        aValues[rw + i, cl + i] += DefensePoints[cComputer];
                                    else aValues[rw + i, cl + i] += AttackPoints[cComputer];
                                    if (CheckPosition(rw - 1, cl - 1) && CheckPosition(rw + 5, cl + 5) && CurrentStatus[rw - 1, cl - 1] == PlayerTurn 
                                        && CurrentStatus[rw + 5, cl + 5] == PlayerTurn)
                                        aValues[rw + i, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((CheckPosition(rw + i - 1, cl + i - 1) && CurrentStatus[rw + i - 1, cl + i - 1] == 0) 
                                    || (CheckPosition(rw + i + 1, cl + i + 1) && CurrentStatus[rw + i + 1, cl + i + 1] == 0)))
                                    aValues[rw + i, cl + i] *= 2;
                                else if (cComputer == 4 || cPlayer == 4)
                                    aValues[rw + i, cl + i] *= 2;
                            }
                        }                         
                }
            }

            // Secondary Diagonal
            for (rw = 4; rw < n; rw++)
            {
                for (cl = 0; cl < n - 4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (CurrentStatus[rw - i, cl + i] == ComputerTurn) 
                            cComputer++;
                        if (CurrentStatus[rw - i, cl + i] == PlayerTurn) 
                            cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                        {
                            if (CurrentStatus[rw - i, cl + i] == 0)
                            {
                                if (cComputer == 0)
                                {
                                    if (CurrentTurn == ComputerTurn) 
                                        aValues[rw - i, cl + i] += DefensePoints[cPlayer];
                                    else aValues[rw - i, cl + i] += AttackPoints[cPlayer];
                                    if (CheckPosition(rw + 1, cl - 1) && CheckPosition(rw - 5, cl + 5) && CurrentStatus[rw + 1, cl - 1] == ComputerTurn 
                                        && CurrentStatus[rw - 5, cl + 5] == ComputerTurn)
                                        aValues[rw - i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (CurrentTurn == PlayerTurn) aValues[rw - i, cl + i] += DefensePoints[cComputer];
                                    else aValues[rw - i, cl + i] += AttackPoints[cComputer];
                                    if (CheckPosition(rw + 1, cl - 1) && CheckPosition(rw - 5, cl + 5) && CurrentStatus[rw + 1, cl - 1] == PlayerTurn 
                                        && CurrentStatus[rw - 5, cl + 5] == PlayerTurn)
                                        aValues[rw + i, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((CheckPosition(rw - i + 1, cl + i - 1) && CurrentStatus[rw - i + 1, cl + i - 1] == 0) 
                                    || (CheckPosition(rw - i - 1, cl + i + 1) && CurrentStatus[rw - i - 1, cl + i + 1] == 0)))
                                    aValues[rw - i, cl + i] *= 2;
                                else if (cComputer == 4 || cPlayer == 4)
                                    aValues[rw - i, cl + i] *= 2;
                            }
                        }                           
                }
            }
                
            Point MaxPoint = new Point(0, 0);
            List<Point> Moves = new List<Point>();
            for (int u = 0; u < Cons.BOARD_HEIGHT; u++)
            {
                for (int v = 0; v < Cons.BOARD_WIDTH; v++)
                {
                    if (aValues[MaxPoint.X, MaxPoint.Y] <= aValues[u, v])
                    {
                        MaxPoint.X = u;
                        MaxPoint.Y = v;
                    }
                }
            }

            for (int u = 0; u < Cons.BOARD_HEIGHT; u++)
            {
                for (int v = 0; v < Cons.BOARD_WIDTH; v++)
                {
                    if (aValues[MaxPoint.X, MaxPoint.Y] * 0.5 <= aValues[u, v])
                        Moves.Add(new Point(u, v));
                }
            }
            return Moves;
        }
    }
}