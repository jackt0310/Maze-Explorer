using UnityEngine;
using System.Collections;

public abstract class MazeAlgorithm {
	protected MazeCell[,] mazeCells;
	protected int mazeRows, mazeColumns;
	// constructor
	protected MazeAlgorithm(MazeCell[,] mazeCells) : base() {
		this.mazeCells = mazeCells;
		// gets maze rows and column length
		// this can be changed by the user in unity if desired
		mazeRows = mazeCells.GetLength(0);
		mazeColumns = mazeCells.GetLength(1);
	}

	public abstract void CreateMaze ();
}
