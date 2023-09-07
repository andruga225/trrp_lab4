namespace lab4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpGameState = new System.Windows.Forms.TableLayoutPanel();
            this.labelYou = new System.Windows.Forms.Label();
            this.labelEnemy = new System.Windows.Forms.Label();
            this.labelyourScore = new System.Windows.Forms.Label();
            this.labelEnemyScore = new System.Windows.Forms.Label();
            this.tlpGameSection = new System.Windows.Forms.TableLayoutPanel();
            this.tlpYourGameSection = new System.Windows.Forms.TableLayoutPanel();
            this.tlpYourBtnsSection = new System.Windows.Forms.TableLayoutPanel();
            this.btnStone = new System.Windows.Forms.Button();
            this.btnShear = new System.Windows.Forms.Button();
            this.btnPaper = new System.Windows.Forms.Button();
            this.picYourChoice = new System.Windows.Forms.PictureBox();
            this.tlpEnemyGameSection = new System.Windows.Forms.TableLayoutPanel();
            this.labelEnemyChoice = new System.Windows.Forms.Label();
            this.picEnemyChoice = new System.Windows.Forms.PictureBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpGameState.SuspendLayout();
            this.tlpGameSection.SuspendLayout();
            this.tlpYourGameSection.SuspendLayout();
            this.tlpYourBtnsSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picYourChoice)).BeginInit();
            this.tlpEnemyGameSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEnemyChoice)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpGameState
            // 
            this.tlpGameState.ColumnCount = 4;
            this.tlpGameState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpGameState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpGameState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpGameState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpGameState.Controls.Add(this.labelYou, 0, 0);
            this.tlpGameState.Controls.Add(this.labelEnemy, 3, 0);
            this.tlpGameState.Controls.Add(this.labelyourScore, 1, 0);
            this.tlpGameState.Controls.Add(this.labelEnemyScore, 2, 0);
            this.tlpGameState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGameState.Location = new System.Drawing.Point(3, 3);
            this.tlpGameState.Name = "tlpGameState";
            this.tlpGameState.RowCount = 1;
            this.tlpGameState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGameState.Size = new System.Drawing.Size(700, 39);
            this.tlpGameState.TabIndex = 0;
            // 
            // labelYou
            // 
            this.labelYou.AutoSize = true;
            this.labelYou.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelYou.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelYou.Location = new System.Drawing.Point(3, 0);
            this.labelYou.Name = "labelYou";
            this.labelYou.Size = new System.Drawing.Size(274, 39);
            this.labelYou.TabIndex = 0;
            this.labelYou.Text = "Вы";
            this.labelYou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEnemy
            // 
            this.labelEnemy.AutoSize = true;
            this.labelEnemy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEnemy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelEnemy.Location = new System.Drawing.Point(423, 0);
            this.labelEnemy.Name = "labelEnemy";
            this.labelEnemy.Size = new System.Drawing.Size(274, 39);
            this.labelEnemy.TabIndex = 1;
            this.labelEnemy.Text = "Противник";
            this.labelEnemy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelyourScore
            // 
            this.labelyourScore.AutoSize = true;
            this.labelyourScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelyourScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelyourScore.Location = new System.Drawing.Point(283, 0);
            this.labelyourScore.Name = "labelyourScore";
            this.labelyourScore.Size = new System.Drawing.Size(64, 39);
            this.labelyourScore.TabIndex = 2;
            this.labelyourScore.Text = "0";
            this.labelyourScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEnemyScore
            // 
            this.labelEnemyScore.AutoSize = true;
            this.labelEnemyScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEnemyScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelEnemyScore.Location = new System.Drawing.Point(353, 0);
            this.labelEnemyScore.Name = "labelEnemyScore";
            this.labelEnemyScore.Size = new System.Drawing.Size(64, 39);
            this.labelEnemyScore.TabIndex = 3;
            this.labelEnemyScore.Text = "0";
            this.labelEnemyScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpGameSection
            // 
            this.tlpGameSection.ColumnCount = 2;
            this.tlpGameSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGameSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGameSection.Controls.Add(this.tlpYourGameSection, 0, 0);
            this.tlpGameSection.Controls.Add(this.tlpEnemyGameSection, 1, 0);
            this.tlpGameSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGameSection.Location = new System.Drawing.Point(3, 48);
            this.tlpGameSection.Name = "tlpGameSection";
            this.tlpGameSection.RowCount = 1;
            this.tlpGameSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGameSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGameSection.Size = new System.Drawing.Size(700, 402);
            this.tlpGameSection.TabIndex = 1;
            // 
            // tlpYourGameSection
            // 
            this.tlpYourGameSection.ColumnCount = 1;
            this.tlpYourGameSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpYourGameSection.Controls.Add(this.tlpYourBtnsSection, 0, 0);
            this.tlpYourGameSection.Controls.Add(this.picYourChoice, 0, 1);
            this.tlpYourGameSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpYourGameSection.Location = new System.Drawing.Point(3, 3);
            this.tlpYourGameSection.Name = "tlpYourGameSection";
            this.tlpYourGameSection.RowCount = 2;
            this.tlpYourGameSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpYourGameSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91F));
            this.tlpYourGameSection.Size = new System.Drawing.Size(344, 396);
            this.tlpYourGameSection.TabIndex = 1;
            // 
            // tlpYourBtnsSection
            // 
            this.tlpYourBtnsSection.ColumnCount = 3;
            this.tlpYourBtnsSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpYourBtnsSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpYourBtnsSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpYourBtnsSection.Controls.Add(this.btnStone, 0, 0);
            this.tlpYourBtnsSection.Controls.Add(this.btnShear, 1, 0);
            this.tlpYourBtnsSection.Controls.Add(this.btnPaper, 2, 0);
            this.tlpYourBtnsSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpYourBtnsSection.Location = new System.Drawing.Point(3, 3);
            this.tlpYourBtnsSection.Name = "tlpYourBtnsSection";
            this.tlpYourBtnsSection.RowCount = 1;
            this.tlpYourBtnsSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpYourBtnsSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpYourBtnsSection.Size = new System.Drawing.Size(338, 29);
            this.tlpYourBtnsSection.TabIndex = 0;
            // 
            // btnStone
            // 
            this.btnStone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStone.Location = new System.Drawing.Point(3, 3);
            this.btnStone.Name = "btnStone";
            this.btnStone.Size = new System.Drawing.Size(106, 23);
            this.btnStone.TabIndex = 0;
            this.btnStone.Text = "Камень";
            this.btnStone.UseVisualStyleBackColor = true;
            this.btnStone.Click += new System.EventHandler(this.btnStone_Click);
            // 
            // btnShear
            // 
            this.btnShear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShear.Location = new System.Drawing.Point(115, 3);
            this.btnShear.Name = "btnShear";
            this.btnShear.Size = new System.Drawing.Size(106, 23);
            this.btnShear.TabIndex = 2;
            this.btnShear.Text = "Ножницы";
            this.btnShear.UseVisualStyleBackColor = true;
            this.btnShear.Click += new System.EventHandler(this.btnShear_Click);
            // 
            // btnPaper
            // 
            this.btnPaper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPaper.Location = new System.Drawing.Point(227, 3);
            this.btnPaper.Name = "btnPaper";
            this.btnPaper.Size = new System.Drawing.Size(108, 23);
            this.btnPaper.TabIndex = 1;
            this.btnPaper.Text = "Бумага";
            this.btnPaper.UseVisualStyleBackColor = true;
            this.btnPaper.Click += new System.EventHandler(this.btnPaper_Click);
            // 
            // picYourChoice
            // 
            this.picYourChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picYourChoice.Location = new System.Drawing.Point(3, 38);
            this.picYourChoice.Name = "picYourChoice";
            this.picYourChoice.Size = new System.Drawing.Size(338, 355);
            this.picYourChoice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picYourChoice.TabIndex = 1;
            this.picYourChoice.TabStop = false;
            // 
            // tlpEnemyGameSection
            // 
            this.tlpEnemyGameSection.ColumnCount = 1;
            this.tlpEnemyGameSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEnemyGameSection.Controls.Add(this.labelEnemyChoice, 0, 0);
            this.tlpEnemyGameSection.Controls.Add(this.picEnemyChoice, 0, 1);
            this.tlpEnemyGameSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEnemyGameSection.Location = new System.Drawing.Point(353, 3);
            this.tlpEnemyGameSection.Name = "tlpEnemyGameSection";
            this.tlpEnemyGameSection.RowCount = 2;
            this.tlpEnemyGameSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpEnemyGameSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91F));
            this.tlpEnemyGameSection.Size = new System.Drawing.Size(344, 396);
            this.tlpEnemyGameSection.TabIndex = 2;
            // 
            // labelEnemyChoice
            // 
            this.labelEnemyChoice.AutoSize = true;
            this.labelEnemyChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEnemyChoice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelEnemyChoice.Location = new System.Drawing.Point(3, 0);
            this.labelEnemyChoice.Name = "labelEnemyChoice";
            this.labelEnemyChoice.Size = new System.Drawing.Size(338, 35);
            this.labelEnemyChoice.TabIndex = 0;
            this.labelEnemyChoice.Text = "Выбор противника";
            this.labelEnemyChoice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picEnemyChoice
            // 
            this.picEnemyChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picEnemyChoice.Location = new System.Drawing.Point(3, 38);
            this.picEnemyChoice.Name = "picEnemyChoice";
            this.picEnemyChoice.Size = new System.Drawing.Size(338, 355);
            this.picEnemyChoice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEnemyChoice.TabIndex = 1;
            this.picEnemyChoice.TabStop = false;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpGameState, 0, 0);
            this.tlpMain.Controls.Add(this.tlpGameSection, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlpMain.Size = new System.Drawing.Size(706, 453);
            this.tlpMain.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 453);
            this.Controls.Add(this.tlpMain);
            this.MinimumSize = new System.Drawing.Size(722, 492);
            this.Name = "Form1";
            this.Text = "Game";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.tlpGameState.ResumeLayout(false);
            this.tlpGameState.PerformLayout();
            this.tlpGameSection.ResumeLayout(false);
            this.tlpYourGameSection.ResumeLayout(false);
            this.tlpYourBtnsSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picYourChoice)).EndInit();
            this.tlpEnemyGameSection.ResumeLayout(false);
            this.tlpEnemyGameSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEnemyChoice)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private TableLayoutPanel tlpGameState;
        private Label labelYou;
        private Label labelEnemy;
        private Label labelyourScore;
        private Label labelEnemyScore;
        private TableLayoutPanel tlpGameSection;
        private TableLayoutPanel tlpYourGameSection;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpYourBtnsSection;
        private Button btnStone;
        private Button btnPaper;
        private Button btnShear;
        private PictureBox picYourChoice;
        private TableLayoutPanel tlpEnemyGameSection;
        private Label labelEnemyChoice;
        private PictureBox picEnemyChoice;
    }
}