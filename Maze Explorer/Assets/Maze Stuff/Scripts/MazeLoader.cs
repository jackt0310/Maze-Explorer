using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public GameObject floors;
	public float size;

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {
		//create maze grid
		InitializeMaze ();
		// call hunt and kill algorithm to knock down random walls and create maze
		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		// create maze from maze algorithm
		ma.CreateMaze ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//this creates a grid of walls, every cell will have a north, south, east, west wall
	private void InitializeMaze() {
        Color replace;

        switch (InventoryManagement.CurrentLevel)
        {
            case 1:
                replace = Color.white;
                break;
            case 2:
                replace = Color.red;
                break;
            case 3:
                replace = Color.blue;
                break;
            case 4:
                // Orange
                replace = new Color(255f / 255f, 69f / 255f, 0f / 255f);
                break;
            default:
                replace = Color.white;
                break;
        }

        // create 2d array to hold each maze cell
        mazeCells = new MazeCell[mazeRows,mazeColumns];
		// go through each cell and create the grid of walls
		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				// create maze cell
				mazeCells [r, c] = new MazeCell ();
				// change this depending on the size you want the hallway
				// make sure floors are big enough for this var
				size = 22f;

				// create floor for each cell
				mazeCells [r, c] .floor = Instantiate (floors, new Vector3 (r*size, 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				// if first column create a west wall
				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 5, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
                    mazeCells[r, c].westWall.GetComponent<Renderer>().material.SetColor("_Color", replace);
                    

                }

				// create east wall for every cell, not west otherwise we would have overlaps
				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 5, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;
                mazeCells[r, c].eastWall.GetComponent<Renderer>().material.SetColor("_Color", replace);

                // if first row create a north wall
                if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 5, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
                    mazeCells[r, c].northWall.GetComponent<Renderer>().material.SetColor("_Color", replace);
                }

				// create a south wall for every cell, not north otherwise we would have overlaps
				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 5, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
                mazeCells[r, c].southWall.GetComponent<Renderer>().material.SetColor("_Color", replace);

                //make door opening for end of maze, always in the same spot bottom right
                if (r == mazeRows-1 && c == mazeColumns-1) {
					Destroy(mazeCells[r,c].southWall);
				}
			}
		}
	}
}
