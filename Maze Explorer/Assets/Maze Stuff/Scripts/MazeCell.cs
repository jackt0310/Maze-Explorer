using UnityEngine;

// properties of each maze cell, used in MazeLoader
public class MazeCell {
	public bool visited = false;
	public GameObject northWall, southWall, eastWall, westWall, floor;
}
