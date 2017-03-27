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
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents an entire mine field.
    /// </summary>
    public class Minefield
    {
        #region Properties

        /// <summary>
        /// A list containing all cells in this field.
        /// The list contains all cells, row by row.
        /// Indexing a cell at position (x, y) is done by taking Cells[y * width + x].
        /// </summary>
        public List<Cell> Cells { get; }

        /// <summary>
        /// The width of the mine field.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height of the mine field.
        /// </summary>
        public int Height { get; }

        #endregion Properties

        #region Serialization and deserialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Minefield" /> class.
        /// </summary>
        /// <param name="fieldInput">A string containing a textual representation of the mine field.
        /// The first line contains the width of the field, the second the height(in number of cells).
        /// This is followed by a comma separated list of numbers representing the number of neighbouring bombs in each cell.
        /// Optionally, these numbers are prefixed with the state of the cell (0 = unknown, 1 = bomb, 2 = safe),
        /// separated with a colon.If no state is provided, unknown is assumed.</param>
        public Minefield(string fieldInput)
        {
            var lines = fieldInput.Split(new[] {'\n'}, 3);

            if (lines.Length < 3) throw new LoadingException("There are not enough lines to parse a mine field. (width/height/data).");

            try
            {
                this.Width = int.Parse(lines[0]);
            }
            catch (FormatException)
            {
                throw new LoadingException("Unable to parse the width.");
            }

            try
            {
                this.Height = int.Parse(lines[1]);
            }
            catch (FormatException)
            {
                throw new LoadingException("Unable to parse the height.");
            }

            var cellDescriptions = lines[2].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            if (cellDescriptions.Length != this.Width * this.Height)
            {
                throw new LoadingException(
                    $"The number of cell descriptions should be width * height, {this.Width * this.Height}, but is {cellDescriptions.Length}");
            }

            this.Cells = new List<Cell>();
            foreach (var cellDescription in cellDescriptions)
            {
                var parts = cellDescription.Split(new[] {':'}, 2);

            try
                {
                    if (parts.Length == 1)
                    {
                        var neighboringBombs = int.Parse(parts[0]);
                        this.Cells.Add(
                            new Cell
                            {
                                NeighboringBombs = neighboringBombs,
                                State = CellState.Unknown
                            });
                    }
                    if (parts.Length == 2)
                    {
                        var stateInt = int.Parse(parts[0]);
                        if (stateInt < 0 || stateInt > 2)
                            throw new LoadingException($"Invalid cell state provided: {stateInt}.");

                        var cellState = (CellState) stateInt;
                        var neighboringBombs = int.Parse(parts[1]);
                        this.Cells.Add(
                            new Cell
                            {
                                NeighboringBombs = neighboringBombs,
                                State = cellState
                            });
                    }
                }
                catch (FormatException)
                {
                    throw new LoadingException($"Unable to parse cell data: '{cellDescription}'.");
                }
            }
        }

        /// <summary>
        /// Serializes the current data for the mine field.
        /// </summary>
        /// <returns>The serialized mine field.</returns>
        public string Serialize()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Width.ToString());
            sb.AppendLine(this.Height.ToString());
            sb.AppendLine(string.Join(",", this.Cells.Select(c => $"{(int) c.State}:{c.NeighboringBombs}")));
            return sb.ToString();
        }

        #endregion Serialization and deserialization

        #region Marking

        /// <summary>
        /// Marks the specified cell to the specified state.
        /// </summary>
        /// <param name="index">The cell index.</param>
        /// <param name="state">The new state.</param>
        public void MarkCell(Point index, CellState state)
        {
            this.IndexToCell(index).State = state;
        }

        #endregion Marking

        #region Solving

        public void TrySolveCell(Point index)
        {
            var x = index.X;
            var y = index.Y;
            var cell = this.IndexToCell(index);

            // Fill a list of neighbors.
            var neighbors = new List<Cell>();
            if (x > 0)
            {
                // Add cells from left
                neighbors.Add(this.IndexToCell(new Point(x - 1, y)));
                if (y > 0) neighbors.Add(this.IndexToCell(new Point(x -1, y -1)));
                if (y < this.Height - 1) neighbors.Add(this.IndexToCell(new Point(x - 1, y + 1)));
            }
            if (x < this.Width - 1)
            {
                // Add cells from right
                neighbors.Add(this.IndexToCell(new Point(x + 1, y)));
                if (y > 0) neighbors.Add(this.IndexToCell(new Point(x + 1, y - 1)));
                if (y < this.Height - 1) neighbors.Add(this.IndexToCell(new Point(x + 1, y + 1)));
            }
            if (y > 0)
            {
                // Add cell from top
                neighbors.Add(this.IndexToCell(new Point(x, y - 1)));
            }
            if (y < this.Height - 1)
            {
                // Add cell from bottom
                neighbors.Add(this.IndexToCell(new Point(x, y + 1)));
            }

            var unknownNeighbors = neighbors.Where(c => c.State == CellState.Unknown).ToList();

            if (!unknownNeighbors.Any()) return;

            var bombed = neighbors.Where(c => c.State == CellState.Bomb);
            var toBeBombed = cell.NeighboringBombs - bombed.Count();

            if (toBeBombed == unknownNeighbors.Count)
            {
                foreach (var n in unknownNeighbors) n.State = CellState.Bomb;
                return;
            }
            if (toBeBombed == 0)
            {
                foreach (var n in unknownNeighbors) n.State = CellState.Safe;
            }
        }

        public void TrySolveEverything()
        {
            for (var i = 0; i < this.Width; i++)
            for (var j = 0; j < this.Height; j++)
                this.TrySolveCell(new Point(i, j));
        }

        #endregion Solving

        /// <summary>
        /// Returns the cell at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The cell at teh specified index.</returns>
        private Cell IndexToCell(Point index)
        {
            var arrIndex = index.Y * this.Width + index.X;
            return this.Cells[arrIndex];
        }
    }
}
