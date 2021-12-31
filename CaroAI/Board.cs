using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CaroAI
{
    class Board
    {

        #region Properties
        private Panel boardPnl;
        public Panel BoardPnl { get => boardPnl; set => boardPnl = value; }

        private Game form;

        private List<Player> player;
        internal List<Player> Player { get => player; set => player = value; }

        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public int ComputerID;

        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }

        private int CountMove;
        private int[,] cells;
        private Stack<CellInfo> playTimeLine;
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

        public bool drawnGame = false;
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


        private bool isPVC;
        public bool IsPVC { get => isPVC; set => isPVC = value; }

        #endregion

        #region Initialize
        public Board(Panel board, Game form)
        {
            this.BoardPnl = board;
            this.form = form;
            this.player = new List<Player>()
            {
                new Player("X", Image.FromFile(Application.StartupPath + "\\Resources\\x.png")),
                new Player("O", Image.FromFile(Application.StartupPath + "\\Resources\\o.png"))
            };
            cells = new int[Cons.BOARD_WIDTH, Cons.BOARD_HEIGHT];
            playTimeLine = new Stack<CellInfo>();


        }
        #endregion

        #region Methods
        public void DrawBoard()
        {
            Console.WriteLine("Drawing board...");
            CurrentPlayer = 0;
            drawnGame = false;
            ComputerID = form.rBtnX.Checked ? 0 : 1;
            form.imgCurrentPlayer.Image = Player[CurrentPlayer].Mark;
            isPVC = form.rBtnPVC.Checked;

            if (isPVC)
            {
                advancedAI = new AdvancedAI(2);
                simpleAI = new SimpleAI(2);
            } 
            else
            {
                advancedAI = new AdvancedAI(1);
                simpleAI = new SimpleAI(2);
            }

            EnableButtonOnBoard();

            // Reset game
            BoardPnl.Controls.Clear();
            playTimeLine.Clear();
            form.tmrCoolDown.Stop();
            form.prbCoolDown.Value = 0;
            Array.Clear(cells, 0, cells.Length);
            CountMove = 0;

            Matrix = new List<List<Button>>();
            Button oldButton = new Button()
            {
                Width = 0,
                Location = new Point(0, 0)
            };

            for (int i = 0; i < Cons.BOARD_HEIGHT; ++i)
            {
                for (int j = 0; j < Cons.BOARD_WIDTH; ++j)
                {
                    Matrix.Add(new List<Button>());
                    Button btn = new Button()
                    {
                        Width = Cons.UNIT,
                        Height = Cons.UNIT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        Tag = i.ToString()
                    };

                    btn.Click += btn_Click;
                    BoardPnl.Controls.Add(btn);

                    Matrix[i].Add(btn);

                    oldButton = btn;
                }
                oldButton = new Button()
                {
                    Width = 0,
                    Height = 0,
                    Location = new Point(0, oldButton.Location.Y + Cons.UNIT)
                };
            }

            if (!isPVC || CurrentPlayer == 1)
            {
                Matrix[new Random().Next(Cons.BOARD_HEIGHT / 3, 2 * Cons.BOARD_WIDTH / 3)][new Random().Next(Cons.BOARD_HEIGHT / 3, 2 * Cons.BOARD_WIDTH / 3)].PerformClick();
            }
        }

        private void EnableButtonOnBoard()
        {
            BoardPnl.Enabled = true;

            //if (isPVC)
            //{
                form.undo_btn.Enabled = true;
                form.help_btn.Enabled = true;
            //}
            // form.grpMode.Enabled = false;
            // form.grpXO.Enabled = false;
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
        private async void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
            {
                if ((isPVC && CurrentPlayer == 1) || (!isPVC))
                {
                    List<Point> list = simpleAI.GetPossibleMoves(cells, CurrentPlayer);
                    Random rnd = new Random();
                    int r = rnd.Next(list.Count);
                    var button = Matrix[list[r].Y][list[r].X];
                    button.PerformClick();
                }                
                return;
            }

            Mark(btn);
            Point point = GetPoint(btn);

            playTimeLine.Push(new CellInfo(point, CurrentPlayer));
            cells[point.X, point.Y] = CurrentPlayer + 1;
            //PrintCurrentBoard();
            ChangeCurrentPlayerMark();
            if (playerMarked != null)
                playerMarked(this, new EventArgs());

            if (isEndGame(btn))
            {
                EndGame();
                return;
            }

            if (isPVC)
            {
                if (CurrentPlayer == 1)
                {
                    await Task.Run(ComputerMove);
                }
            }

            else
            {
                await Task.Delay(500);
                await Task.Run(ComputerMove);
            }
        }
        private void ComputerMove()
        {
            Button btn;
            Point point;
            var BoardClone = cells.Clone() as int[,];
            if (isPVC)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                if (ComputerID == 0) point = simpleAI.FindBestMove(BoardClone, CurrentPlayer + 1);
                else point = advancedAI.FindBestMove(BoardClone, CurrentPlayer + 1);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Calculated after " + elapsedMs + " ms");
            }
            else
            {
                if (CurrentPlayer == 0)
                {
                    point = advancedAI.FindBestMove(BoardClone, CurrentPlayer + 1);
                }
                else
                {
                    point = simpleAI.FindBestMove(BoardClone, CurrentPlayer + 1);
                }
            }
            btn = Matrix[point.Y][point.X];
            form.Invoke(new Action(() => { 
                btn.PerformClick();
                btn.Focus();
            }));
        }

        private void ChangeCurrentPlayerMark()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            form.imgCurrentPlayer.Image = Player[CurrentPlayer].Mark;
        }

        private void Mark(Button btn)
        {
            CountMove++;
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public bool Undo()
        {
            if (isPVC)
            {
                if (playTimeLine.Count <= 0)
                    return false;
                form.prbCoolDown.Value = 0;
                CellInfo oldInfo = playTimeLine.Pop();
                Button btn = Matrix[oldInfo.Point.Y][oldInfo.Point.X];
                btn.BackgroundImage = null;
                cells[oldInfo.Point.X, oldInfo.Point.Y] = 0;

                if (playTimeLine.Count <= 0)
                {
                    CurrentPlayer = 0;
                }
                else
                {
                    oldInfo = playTimeLine.Pop();
                    btn = Matrix[oldInfo.Point.Y][oldInfo.Point.X];
                    btn.BackgroundImage = null;
                    cells[oldInfo.Point.X, oldInfo.Point.Y] = 0;
                }
                return true;
            }
            return false;
        }
        
        public void HelpPlayerMove()
        {
            var pnt = simpleAI.FindBestMove(cells, CurrentPlayer);
            var btn = Matrix[pnt.Y][pnt.X];
            btn.Focus();
        }

        #region check end game
        public void EndGame()
        {
            if (isDraw()) drawnGame = true;
            if (endedGame != null)
                endedGame(this, new EventArgs());
        }
        private bool isEndGame(Button btn)
        {
            return isDraw() || isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimaryDiagonal(btn) || isEndSecondaryDiagonal(btn);
        }

        private Point GetPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }

        private bool isDraw()
        {
            return CountMove >= Cons.BOARD_HEIGHT * Cons.BOARD_WIDTH;
        }
        private bool isEndHorizontal(Button btn)
        {
            Point point = GetPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; --i)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Cons.BOARD_WIDTH; ++i)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
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
                    if (Matrix[point.Y][point.X + countRight + 1].BackgroundImage == null)
                        return true;
                    return false;
                }

                if (point.X + countRight + 1 >= Cons.BOARD_WIDTH)
                {
                    if (Matrix[point.Y][point.X - countLeft].BackgroundImage == null)
                        return true;
                    return false;
                }
                if (Matrix[point.Y][point.X - countLeft].BackgroundImage == null || Matrix[point.Y][point.X + countRight + 1].BackgroundImage == null)
                    return true;               
            }
               
            return false;
        }
        private bool isEndVertical(Button btn)
        {
            Point point = GetPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; --i)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.BOARD_HEIGHT; ++i)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
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
                    if (Matrix[point.Y + countBottom + 1][point.X].BackgroundImage == null)
                        return true;
                    return false;
                }

                if (point.Y + countBottom + 1 >= Cons.BOARD_HEIGHT)
                {
                    if (Matrix[point.Y - countTop][point.X].BackgroundImage == null)
                        return true;
                    return false;
                }
                if (Matrix[point.Y - countTop][point.X].BackgroundImage == null || Matrix[point.Y + countBottom + 1][point.X].BackgroundImage == null)
                    return true;
            }
            return  false;
        }
        private bool isEndPrimaryDiagonal(Button btn)
        {
            Point point = GetPoint(btn);

            int countUp = 0;
            for (int i = 0; i <= point.X; ++i)
            {
                if (point.X - i < 0 || point.Y - i < 0) break; 
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
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
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
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
                    if (Matrix[point.Y + countDown + 1][point.X + countDown + 1].BackgroundImage == null)
                        return true;
                    return false;
                }

                if (point.X + countDown + 1 >= Cons.BOARD_WIDTH || point.Y + countDown + 1 >= Cons.BOARD_HEIGHT)
                {
                    if (Matrix[point.Y - countUp][point.X - countUp].BackgroundImage == null)
                        return true;
                    return false;
                }
                if (Matrix[point.Y - countUp][point.X - countUp].BackgroundImage == null || Matrix[point.Y + countDown + 1][point.X + countDown + 1].BackgroundImage == null)
                    return true;
            }

            return false;
        }
        private bool isEndSecondaryDiagonal(Button btn)
        {
            Point point = GetPoint(btn);

            int countUp = 0;
            for (int i = 0; i <= point.Y; ++i)
            {
                if (point.X + i >= Cons.BOARD_WIDTH || point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
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
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
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
                    if (Matrix[point.Y - countUp][point.X + countUp].BackgroundImage == null)
                        return true;
                    return false;
                }

                if (point.X + countUp >= Cons.BOARD_WIDTH || point.Y - countUp < 0)
                {
                    if (Matrix[point.Y + countDown + 1][point.X - countDown - 1].BackgroundImage == null)
                        return true;
                    return false;
                }
                if (Matrix[point.Y + countDown + 1][point.X - countDown - 1].BackgroundImage == null || Matrix[point.Y - countUp][point.X + countUp].BackgroundImage == null)
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
