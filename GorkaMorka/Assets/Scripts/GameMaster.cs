using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	// Assigning a unique number to each tile
	public int uniqueTileCounter = 0;
	// Dont touch

	public int hexSize;
	public int tileCount;
	public float layerDistance;

	//public int[] playerPositionXY = new int [2];
	//public int playerOnTileNum;


	// Use this for initialization
	void Start () {

		tileCount = hexSize * hexSize;

	//	playerOnTileNum = ((playerPositionXY [0]-1) * hexSize) + (playerPositionXY [1]);

	}



	void Update() {

	
	}

}
