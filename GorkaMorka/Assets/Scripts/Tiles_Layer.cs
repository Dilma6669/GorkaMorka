using UnityEngine;
using System.Collections;

public class Tiles_Layer : MonoBehaviour {

	GameMaster gameMaster;

	// Creating a new Hex Layer
	public int [,] HexGrid;
	private int hexSize;
	private int tileCount;
	private float layerDistance;


	// Tile Layer Prefabs
	public Transform clearTilesPrefab;



	// Use this for initialization
	void Start () {

		// Creating a new Hex Layer
		// BUT working with TILES as apposed to hex values so index's start at 1 // 
		gameMaster = GameObject.Find ("GameMaster").transform.GetComponent<GameMaster> ();
		hexSize = gameMaster.hexSize+1;
		HexGrid = new int [hexSize , hexSize];
		layerDistance = gameMaster.layerDistance;


		//Debug.Log("player[0] = " + gameMaster.playerPositionXY [0] + "player[1] = " + gameMaster.playerPositionXY [1]);
		//playerOnTileNum = ((gameMaster.playerPositionXY [0]-1) * hexSize) + (gameMaster.playerPositionXY [1]);

		// Fill Hex with map tiles corresponding to integer values
		PopulateHex ();

	}




	// Fill Hex with map tiles corresponding to integer values
	void PopulateHex() {

		// Geographical Placement of tiles 
		float colPlace = 0;
		float rowPlace = 0;

		// Row and Column Count 
		int rowCount = 1;
		int colCount = 1;

		// hex rows sitting at different positions
		int offset = 0;

		// X value
		int colLength = hexSize;
		// Y value
		int rowLength = hexSize;

		// Assigns Tiles to Grid Numbers
		// Rows iterator
		for (int i = 1; i < rowLength; i++) {
			// Columns iterator
			for (int j = 1; j < colLength; j++) {

				if (HexGrid [rowCount, colCount] == 0) {
					Transform clear_Tile = Instantiate (clearTilesPrefab);
					clear_Tile.transform.parent = GameObject.Find ("Tiles_Layer").transform;

					clear_Tile.transform.Translate (new Vector3 (colPlace, rowPlace, layerDistance));
				}

				colPlace += 1.4f;
				colCount++;
			}

			// For aligning the Hex tiles
			if (offset == 0) {
				colPlace = 0.7f;
				rowPlace += 1.2f;
				offset = 1;
			} else {
				colPlace = 0f;
				rowPlace += 1.2f;
				offset = 0;
			}

			rowCount++;
			colCount = 1;

		}
	}
		
}
