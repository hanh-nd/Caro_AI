using System;
using System.Windows.Forms;

namespace CaroAI
{
    public partial class Game : Form
    {
        #region Properties
        private Board board;
        private bool isEnded = true;
        #endregion

        public Game()
        {
            InitializeComponent();
            board = new Board(pnlCaroBoard, this);
            board.EndedGame += Board_EndedGame;
            board.PlayerMarked += Board_PlayerMarked;
            InitializeCoolDownProperties();
        }

        private void Board_PlayerMarked(object sender, EventArgs e)
        {
            tmrCoolDown.Start();
            prbCoolDown.Value = 0;
        }

        void EndedGame()
        {
            this.btnPlay.Text = "Chơi";
            tmrCoolDown.Stop();
            prbCoolDown.Value = 0;
            pnlCaroBoard.Enabled = false;
            undo_btn.Enabled = false;
            help_btn.Enabled = false;
            grpMode.Enabled = true;
            if (rBtnCVC.Checked != true)
                grpXO.Enabled = true;
            isEnded = true;
            btnPlay.Click -= new EventHandler(btnEnd_Click);
            btnPlay.Click += new EventHandler(btnPlay_Click);
        }
        private void Board_EndedGame(object sender, EventArgs e)
        {
            EndedGame();
            string winner = board.CurrentPlayer == 1 ? "X" : "O";
            MessageBox.Show($"Kết thúc. {winner} chiến thắng.");
        }

        private void InitializeCoolDownProperties()
        {
            prbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prbCoolDown.Value = 0;
            tmrCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;
        }

        private void undo_Click(object sender, EventArgs e)
        {
            board.Undo();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Text = "Huỷ ván";
            isEnded = false;
            grpMode.Enabled = false;
            grpXO.Enabled = false;
            board.DrawBoard();
            btn.Click -=  new EventHandler(btnPlay_Click);
            btn.Click += new EventHandler(btnEnd_Click);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (!isEnded)
            {
                EndedGame();
            }
            Button btn = sender as Button;
            btn.Text = "Chơi";
            MessageBox.Show($"Kết thúc.");
        }

        private void tmrCoolDown_Tick(object sender, EventArgs e)
        {
            prbCoolDown.PerformStep();

            if (prbCoolDown.Value >= prbCoolDown.Maximum)
            {
                EndedGame();
            }
        }

        void Quit()
        {
            Application.Exit();
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Em có chắc không?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                e.Cancel = true;
        }

        private void rBtnCVC_CheckedChanged(object sender, EventArgs e)
        {
            this.grpXO.Enabled = grpXO.Enabled != true;
            this.undo_btn.Enabled = undo_btn.Enabled != true;
            this.help_btn.Enabled = help_btn.Enabled != true;
        }

        private void help_btn_Click(object sender, EventArgs e)
        {
            board.HelpPlayerMove();
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastToolStripMenuItem.Checked = true;
            slowToolStripMenuItem.Checked = false;
            board.ComputerSpeed = 0;
        }

        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastToolStripMenuItem.Checked = false;
            slowToolStripMenuItem.Checked = true;
            board.ComputerSpeed = Cons.COMPUTER_DELAY;
        }

        private void Game_Load(object sender, EventArgs e)
        {
            Cons.MAX_DEPTH = 3;
        }


        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = true;
            mediumToolStripMenuItem.Checked = false;
            Cons.MAX_DEPTH = 3;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = true;
            Cons.MAX_DEPTH = 4;
        }
    }
}
