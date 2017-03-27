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
    /// <summary>
    /// Represents a single cell.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The state of the cell.
        /// </summary>
       public CellState State { get; set; }

        /// <summary>
        /// The number of neighboring bombs.
        /// </summary>
        public int NeighboringBombs { get; set; }
    }
}
