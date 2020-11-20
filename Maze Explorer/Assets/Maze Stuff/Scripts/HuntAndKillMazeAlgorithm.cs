using UnityEngine;
using System.Collections;

public class HuntAndKillMazeAlgorithm : MazeAlgorithm {

	private int currentRow = 0;
	private int currentColumn = 0;

	private bool courseComplete = false;

	public HuntAndKillMazeAlgorithm(MazeCell[,] mazeCells) : base(mazeCells) {
	}

	public override void CreateMaze () {
		HuntAndKill ();
	}

	private void HuntAndKill() {
		// set 0,0 to be visited so to not revisit
		mazeCells [currentRow, currentColumn].visited = true;
		// repeat process until all cells have been visited and paths are made
		while (! courseComplete) {
			// gets rid of adjascent random wall until it hits a dead end
			Kill();
			// finds the next unvisited cell to start knocking down walls
			Hunt();
		}
	}

	private void Kill() {
		while (RouteStillAvailable (currentRow, currentColumn)) {
			// choose rand num to decide direction 1=north 2=south 3=east 4=west
			int direction = Random.Range (1, 5);
			// North
			if (direction == 1 && CellIsAvailable (currentRow - 1, currentColumn)) {
				// all north walls are border walls, destroy the south wall of the row above
				DestroyWallIfItExists (mazeCells [currentRow - 1, currentColumn].southWall);
				currentRow--;
			} else if (direction == 2 && CellIsAvailable (currentRow + 1, currentColumn)) {
				// South
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn].southWall);
				currentRow++;
			} else if (direction == 3 && CellIsAvailable (currentRow, currentColumn + 1)) {
				// east
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn].eastWall);
				currentColumn++;
			} else if (direction == 4 && CellIsAvailable (currentRow, currentColumn - 1)) {
				// west
				// all west walls are border walls, destroy the east wall of the column to the left
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn - 1].eastWall);
				currentColumn--;
			}
			// set the next corresponding cell to visited
			mazeCells [currentRow, currentColumn].visited = true;
		}
	}

	//search left to right up to down to find a new unvisited cell
	private void Hunt() {
		//if nothing is found then this bool ends the algorithm
		courseComplete = true;
		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				// if it finds an unvisited cell destroy a wall and enter kill phase again
				if (!mazeCells [r, c].visited && CellHasAnAdjacentVisitedCell(r,c)) {
					courseComplete = false;
					currentRow = r;
					currentColumn = c;
					DestroyAdjacentWall (currentRow, currentColumn);
					mazeCells [currentRow, currentColumn].visited = true;
					return;
				}
			}
		}
	}

	// check to see if adjacent cells are unvisited, if yes kill algorithm continues
	private bool RouteStillAvailable(int row, int column) {
		int availableRoutes = 0;
		// above
		if (row > 0 && !mazeCells[row-1,column].visited) {
			availableRoutes++;
		}
		// below
		if (row < mazeRows - 1 && !mazeCells [row + 1, column].visited) {
			availableRoutes++;
		}
		//left
		if (column > 0 && !mazeCells[row,column-1].visited) {
			availableRoutes++;
		}
		// right
		if (column < mazeColumns-1 && !mazeCells[row,column+1].visited) {
			availableRoutes++;
		}

		return availableRoutes > 0;
	}

	// if cell is unvisited and inside the maze area
	private bool CellIsAvailable(int row, int column) {
		if (row >= 0 && row < mazeRows && column >= 0 && column < mazeColumns && !mazeCells [row, column].visited) {
			return true;
		} else {
			return false;
		}
	}

	// checks to make sure wall has not already been destroyed
	private void DestroyWallIfItExists(GameObject wall) {
		if (wall != null) {
			GameObject.Destroy (wall);
		}
	}

	// similar but opposite to route still available used in hunt algo to see if cell checked can be used
	private bool CellHasAnAdjacentVisitedCell(int row, int column) {
		int visitedCells = 0;

		// above
		if (row > 0 && mazeCells [row - 1, column].visited) {
			visitedCells++;
		}

		// below
		if (row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
			visitedCells++;
		}

		// left
		if (column > 0 && mazeCells [row, column - 1].visited) {
			visitedCells++;
		}

		// right
		if (column < (mazeColumns-2) && mazeCells [row, column + 1].visited) {
			visitedCells++;
		}

		return visitedCells > 0;
	}

	// same as kill algo, used in hunt algo to continue to kill
	private void DestroyAdjacentWall(int row, int column) {
		bool wallDestroyed = false;

		while (!wallDestroyed) {
			int direction = Random.Range (1, 5);

			if (direction == 1 && row > 0 && mazeCells [row - 1, column].visited) {
				// all north walls are border walls, destroy the south wall of the row above
				DestroyWallIfItExists (mazeCells [row - 1, column].southWall);
				wallDestroyed = true;
			} else if (direction == 2 && row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
				DestroyWallIfItExists (mazeCells [row, column].southWall);
				wallDestroyed = true;
			} else if (direction == 3 && column > 0 && mazeCells [row, column-1].visited) {
				// all west walls are border walls, destroy the east wall of the column to the left
				DestroyWallIfItExists (mazeCells [row, column-1].eastWall);
				wallDestroyed = true;
			} else if (direction == 4 && column < (mazeColumns-2) && mazeCells [row, column+1].visited) {
				DestroyWallIfItExists (mazeCells [row, column].eastWall);
				wallDestroyed = true;
			}
		}

	}

}
