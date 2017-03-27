/*
Copyright (C) 2017 by Eric Bataille <e.c.p.bataille@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace MinesweeperSolver
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields
        
        /// <summary>
        /// The mine field to solve.
        /// </summary>
        private Minefield field;

        /// <summary>
        /// The last folder used to open a file from.
        /// </summary>
        private string lastFolder;

        /// <summary>
        /// The index at which the menu was opened.
        /// </summary>
        private Point? menuIndex;

        #endregion Fields

        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Draws the current minefield.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightGray);

            if (this.field != null)
            for (var i = 0; i < this.field.Cells.Count; i++)
            {
                var x = i % this.field.Width;
                var y = i / this.field.Width;
                var cell = this.field.Cells[i];

                var color = cell.State == CellState.Unknown
                    ? Color.Gray
                    : cell.State == CellState.Bomb
                        ? Color.Red
                        : Color.Green;

                var cellRect = new Rectangle(50 + x * 40 + 0, 50 + y * 40 + 0, 38 + 2, 38 + 2);
                e.Graphics.FillRectangle(new SolidBrush(color), cellRect);
                    e.Graphics.SetClip(cellRect);
                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(
                        cell.NeighboringBombs.ToString(),
                        new Font("Arial", 24),
                        Brushes.Black,
                        cellRect,
                        stringFormat);
                    e.Graphics.ResetClip();
                }


            base.OnPaint(e);
        }

        #region Menu
        
        private void mnuLoad_Click(object sender, System.EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = this.lastFolder ?? Environment.SpecialFolder.MyDocuments.ToString()
            };

            var result = dialog.ShowDialog(this);
            if (result != DialogResult.OK) return;

            this.lastFolder = Path.GetDirectoryName(dialog.FileName);
            try
            {
                var inputData = File.ReadAllText(dialog.FileName);

                this.field = new Minefield(inputData);
                this.ClientSize = new Size(this.field.Width * 40 + 100, this.field.Height * 40 + 100);
            }
            catch (LoadingException ex)
            {
                MessageBox.Show(this, ex.Message);
                this.field = null;
            }

            this.menuIndex = null;
            this.Refresh();

            Debug.WriteLine(this.field.Cells.Where(x => x.State == CellState.Bomb).Sum(x => x.NeighboringBombs));
            Debug.WriteLine(this.field.Cells.Where(x => x.State == CellState.Safe).Sum(x => x.NeighboringBombs));
        }

        private void mnuSave_Click(object sender, System.EventArgs e)
        {
            if (!this.AssertFieldLoaded()) return;

            using (var dialog = new SaveFileDialog
            {
                InitialDirectory = this.lastFolder ?? Environment.SpecialFolder.MyDocuments.ToString()
            })
            {
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK) return;

                this.lastFolder = Path.GetDirectoryName(dialog.FileName);

                try
                {
                    File.WriteAllText(dialog.FileName, this.field.Serialize());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Unable to save the field: {ex.Message}");
                }
            }

            this.menuIndex = null;
        }

        private void mnuMarkBomb_Click(object sender, System.EventArgs e)
        {
            if (!this.AssertFieldLoaded()) return;
            this.field.MarkCell(this.menuIndex.Value, CellState.Bomb);

            this.menuIndex = null;
            this.Refresh();
        }

        private void mnuMarkSafe_Click(object sender, System.EventArgs e)
        {
            if (!this.AssertFieldLoaded()) return;
            this.field.MarkCell(this.menuIndex.Value, CellState.Safe);

            this.menuIndex = null;
            this.Refresh();
        }

        private void mnuUnmark_Click(object sender, System.EventArgs e)
        {
            if (!this.AssertFieldLoaded()) return;
            this.field.MarkCell(this.menuIndex.Value, CellState.Unknown);

            this.menuIndex = null;
            this.Refresh();
        }

        private void mnuTrySolveCell_Click(object sender, System.EventArgs e)
        {
            if (!this.AssertFieldLoaded()) return;
            this.field.TrySolveCell(this.menuIndex.Value);

            this.menuIndex = null;
            this.Refresh();
        }

        private void mnuTrySolveEverything_Click(object sender, System.EventArgs e)
        {
            if (!this.AssertFieldLoaded()) return;
            this.field.TrySolveEverything();

            this.menuIndex = null;
            this.Refresh();
        }

        private void mnuContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.menuIndex = this.GetHighlightedIndex();
        }

        #endregion Menu

        #region Helpers

        /// <summary>
        /// Retrieves the cell under the cursor.
        /// </summary>
        /// <returns>The cell under the cursor.</returns>
        /// <remarks>Please ensure that the minefield is loaded before calling this function,
        /// it does not do another check.</remarks>
        private Point GetHighlightedIndex()
        {
            var coords = this.PointToClient(Cursor.Position);
            return new Point(coords.X / 40, coords.Y / 40);
        }

        /// <summary>
        /// Asserts that there is a field loaded, shows a message otherwise.
        /// </summary>
        /// <returns>Whether a field is loaded.</returns>
        private bool AssertFieldLoaded()
        {
            if (this.field == null)
            {
                MessageBox.Show(this, "There is no field loaded.");
                return false;
            }

            return true;
        }

        #endregion Helpers
    }
}
