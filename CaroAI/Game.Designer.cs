
namespace CaroAI
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.pnlCaroBoard = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.rBtnCVC = new System.Windows.Forms.RadioButton();
            this.rBtnPVC = new System.Windows.Forms.RadioButton();
            this.btnPlay = new System.Windows.Forms.Button();
            this.grpXO = new System.Windows.Forms.GroupBox();
            this.imgCurrentPlayer = new System.Windows.Forms.PictureBox();
            this.rBtnO = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rBtnX = new System.Windows.Forms.RadioButton();
            this.prbCoolDown = new System.Windows.Forms.ProgressBar();
            this.help_btn = new System.Windows.Forms.Button();
            this.undo_btn = new System.Windows.Forms.Button();
            this.tmrCoolDown = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.độKhóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlInfo.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.grpXO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCurrentPlayer)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCaroBoard
            // 
            this.pnlCaroBoard.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCaroBoard.Location = new System.Drawing.Point(9, 30);
            this.pnlCaroBoard.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCaroBoard.Name = "pnlCaroBoard";
            this.pnlCaroBoard.Size = new System.Drawing.Size(531, 502);
            this.pnlCaroBoard.TabIndex = 0;
            // 
            // pnlLogo
            // 
            this.pnlLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLogo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogo.BackgroundImage")));
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.Location = new System.Drawing.Point(543, 30);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(260, 227);
            this.pnlLogo.TabIndex = 1;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfo.Controls.Add(this.grpMode);
            this.pnlInfo.Controls.Add(this.btnPlay);
            this.pnlInfo.Controls.Add(this.grpXO);
            this.pnlInfo.Controls.Add(this.prbCoolDown);
            this.pnlInfo.Controls.Add(this.help_btn);
            this.pnlInfo.Controls.Add(this.undo_btn);
            this.pnlInfo.Location = new System.Drawing.Point(543, 254);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(261, 278);
            this.pnlInfo.TabIndex = 2;
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.rBtnCVC);
            this.grpMode.Controls.Add(this.rBtnPVC);
            this.grpMode.Location = new System.Drawing.Point(0, 8);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(261, 78);
            this.grpMode.TabIndex = 9;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "Chế độ";
            // 
            // rBtnCVC
            // 
            this.rBtnCVC.AutoSize = true;
            this.rBtnCVC.Location = new System.Drawing.Point(6, 53);
            this.rBtnCVC.Name = "rBtnCVC";
            this.rBtnCVC.Size = new System.Drawing.Size(134, 21);
            this.rBtnCVC.TabIndex = 10;
            this.rBtnCVC.Text = "Máy 01 - Máy 02";
            this.rBtnCVC.UseVisualStyleBackColor = true;
            this.rBtnCVC.CheckedChanged += new System.EventHandler(this.rBtnCVC_CheckedChanged);
            // 
            // rBtnPVC
            // 
            this.rBtnPVC.AutoSize = true;
            this.rBtnPVC.Checked = true;
            this.rBtnPVC.Location = new System.Drawing.Point(6, 28);
            this.rBtnPVC.Name = "rBtnPVC";
            this.rBtnPVC.Size = new System.Drawing.Size(125, 21);
            this.rBtnPVC.TabIndex = 9;
            this.rBtnPVC.TabStop = true;
            this.rBtnPVC.Text = "Người - Máy 01";
            this.rBtnPVC.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(0, 212);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(261, 31);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Chơi";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // grpXO
            // 
            this.grpXO.Controls.Add(this.imgCurrentPlayer);
            this.grpXO.Controls.Add(this.rBtnO);
            this.grpXO.Controls.Add(this.label3);
            this.grpXO.Controls.Add(this.rBtnX);
            this.grpXO.Location = new System.Drawing.Point(0, 92);
            this.grpXO.Name = "grpXO";
            this.grpXO.Size = new System.Drawing.Size(261, 85);
            this.grpXO.TabIndex = 0;
            this.grpXO.TabStop = false;
            this.grpXO.Text = "Máy 01";
            // 
            // imgCurrentPlayer
            // 
            this.imgCurrentPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCurrentPlayer.Location = new System.Drawing.Point(172, 23);
            this.imgCurrentPlayer.Name = "imgCurrentPlayer";
            this.imgCurrentPlayer.Size = new System.Drawing.Size(50, 50);
            this.imgCurrentPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCurrentPlayer.TabIndex = 8;
            this.imgCurrentPlayer.TabStop = false;
            // 
            // rBtnO
            // 
            this.rBtnO.AutoSize = true;
            this.rBtnO.Location = new System.Drawing.Point(6, 55);
            this.rBtnO.Name = "rBtnO";
            this.rBtnO.Size = new System.Drawing.Size(54, 21);
            this.rBtnO.TabIndex = 4;
            this.rBtnO.Text = "AI-2";
            this.rBtnO.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hiện tại";
            // 
            // rBtnX
            // 
            this.rBtnX.AutoSize = true;
            this.rBtnX.Checked = true;
            this.rBtnX.Location = new System.Drawing.Point(6, 28);
            this.rBtnX.Name = "rBtnX";
            this.rBtnX.Size = new System.Drawing.Size(54, 21);
            this.rBtnX.TabIndex = 3;
            this.rBtnX.TabStop = true;
            this.rBtnX.Text = "AI-1";
            this.rBtnX.UseVisualStyleBackColor = true;
            // 
            // prbCoolDown
            // 
            this.prbCoolDown.Location = new System.Drawing.Point(11, 183);
            this.prbCoolDown.Name = "prbCoolDown";
            this.prbCoolDown.Size = new System.Drawing.Size(238, 23);
            this.prbCoolDown.TabIndex = 7;
            // 
            // help_btn
            // 
            this.help_btn.Location = new System.Drawing.Point(143, 249);
            this.help_btn.Name = "help_btn";
            this.help_btn.Size = new System.Drawing.Size(118, 31);
            this.help_btn.TabIndex = 1;
            this.help_btn.Text = "Gợi ý";
            this.help_btn.UseVisualStyleBackColor = true;
            this.help_btn.Click += new System.EventHandler(this.help_btn_Click);
            // 
            // undo_btn
            // 
            this.undo_btn.Location = new System.Drawing.Point(0, 249);
            this.undo_btn.Name = "undo_btn";
            this.undo_btn.Size = new System.Drawing.Size(119, 31);
            this.undo_btn.TabIndex = 1;
            this.undo_btn.Text = "Undo";
            this.undo_btn.UseVisualStyleBackColor = true;
            this.undo_btn.Click += new System.EventHandler(this.undo_Click);
            // 
            // tmrCoolDown
            // 
            this.tmrCoolDown.Tick += new System.EventHandler(this.tmrCoolDown_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(827, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.độKhóToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // độKhóToolStripMenuItem
            // 
            this.độKhóToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem,
            this.mediumToolStripMenuItem});
            this.độKhóToolStripMenuItem.Name = "độKhóToolStripMenuItem";
            this.độKhóToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.độKhóToolStripMenuItem.Text = "Độ khó";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.Checked = true;
            this.easyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.easyToolStripMenuItem.Text = "Dễ";
            this.easyToolStripMenuItem.Click += new System.EventHandler(this.easyToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.mediumToolStripMenuItem.Text = "Khó";
            this.mediumToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 541);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.pnlCaroBoard);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(845, 588);
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CaroAI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.Load += new System.EventHandler(this.Game_Load);
            this.pnlInfo.ResumeLayout(false);
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.grpXO.ResumeLayout(false);
            this.grpXO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCurrentPlayer)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCaroBoard;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RadioButton rBtnO;
        public System.Windows.Forms.RadioButton rBtnX;
        public System.Windows.Forms.PictureBox imgCurrentPlayer;
        private System.Windows.Forms.RadioButton rBtnCVC;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        public System.Windows.Forms.Timer tmrCoolDown;
        public System.Windows.Forms.ProgressBar prbCoolDown;
        public System.Windows.Forms.Button help_btn;
        public System.Windows.Forms.Button undo_btn;
        public System.Windows.Forms.GroupBox grpMode;
        public System.Windows.Forms.GroupBox grpXO;
        public System.Windows.Forms.RadioButton rBtnPVC;
        private System.Windows.Forms.ToolStripMenuItem độKhóToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
    }
}

