namespace MinesweeperSolver
{
    partial class MainForm
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
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMarkBomb = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMarkSafe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUnmark = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTrySolveCell = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrySolveEverything = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLoad,
            this.mnuSave,
            this.toolStripSeparator1,
            this.mnuMarkBomb,
            this.mnuMarkSafe,
            this.mnuUnmark,
            this.toolStripSeparator2,
            this.mnuTrySolveCell,
            this.mnuTrySolveEverything});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(181, 192);
            this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
            // 
            // mnuLoad
            // 
            this.mnuLoad.Name = "mnuLoad";
            this.mnuLoad.Size = new System.Drawing.Size(180, 22);
            this.mnuLoad.Text = "&Load";
            this.mnuLoad.Click += new System.EventHandler(this.mnuLoad_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(180, 22);
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuMarkBomb
            // 
            this.mnuMarkBomb.Name = "mnuMarkBomb";
            this.mnuMarkBomb.Size = new System.Drawing.Size(180, 22);
            this.mnuMarkBomb.Text = "Mark &Bomb";
            this.mnuMarkBomb.Click += new System.EventHandler(this.mnuMarkBomb_Click);
            // 
            // mnuMarkSafe
            // 
            this.mnuMarkSafe.Name = "mnuMarkSafe";
            this.mnuMarkSafe.Size = new System.Drawing.Size(180, 22);
            this.mnuMarkSafe.Text = "Mark Sa&fe";
            this.mnuMarkSafe.Click += new System.EventHandler(this.mnuMarkSafe_Click);
            // 
            // mnuUnmark
            // 
            this.mnuUnmark.Name = "mnuUnmark";
            this.mnuUnmark.Size = new System.Drawing.Size(180, 22);
            this.mnuUnmark.Text = "&Unmark";
            this.mnuUnmark.Click += new System.EventHandler(this.mnuUnmark_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuTrySolveCell
            // 
            this.mnuTrySolveCell.Name = "mnuTrySolveCell";
            this.mnuTrySolveCell.Size = new System.Drawing.Size(180, 22);
            this.mnuTrySolveCell.Text = "Try Solve &Cell";
            this.mnuTrySolveCell.Click += new System.EventHandler(this.mnuTrySolveCell_Click);
            // 
            // mnuTrySolveEverything
            // 
            this.mnuTrySolveEverything.Name = "mnuTrySolveEverything";
            this.mnuTrySolveEverything.Size = new System.Drawing.Size(180, 22);
            this.mnuTrySolveEverything.Text = "Try Solve &Everything";
            this.mnuTrySolveEverything.Click += new System.EventHandler(this.mnuTrySolveEverything_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 441);
            this.ContextMenuStrip = this.mnuContext;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Minesweeper Solver";
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuLoad;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuMarkBomb;
        private System.Windows.Forms.ToolStripMenuItem mnuMarkSafe;
        private System.Windows.Forms.ToolStripMenuItem mnuUnmark;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuTrySolveCell;
        private System.Windows.Forms.ToolStripMenuItem mnuTrySolveEverything;
    }
}

