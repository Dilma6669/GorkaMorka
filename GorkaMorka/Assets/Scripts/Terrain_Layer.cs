using UnityEngine;
using System.Collections;

public class Terrain_Layer : MonoBehaviour {

	GameMaster gameMaster;

	// Creating a new Hex Layer
	public int [,] HexGrid;
	private int hexSize;
	private int tileCount;
	private float layerDistance;


	// Terrain Layer Prefabs
	public Transform[] mountainPrefab;
	public Transform[] rock_RoughPrefab;
	public Transform[] rock_LightPrefab;
	public Transform[] sand_RoughPrefab;
	public Transform[] sand_LightPrefab;


	public int numberOfMountains;
	public int minSizeOfMountains;
	public int maxSizeOfMountains;

	public int minSizeOfRocks;
	public int maxSizeOfRocks;

	// Use this for initialization
	void Start () {

		gameMaster = GameObject.Find ("GameMaster").transform.GetComponent<GameMaster> ();
		hexSize = gameMaster.hexSize;
		HexGrid = new int [hexSize , hexSize];

		//tileCount = hexSize * hexSize;

		// Fill Hex with integer values that will correspond to map tiles
		FillHex ();

		// Fill Hex with map tiles corresponding to integer values
		PopulateHex ();

	}




	// Fill Hex with integer values that will correspond to map tiles
	void FillHex() {

		// X value
		int colLength = hexSize-1;
		// Y value
		int rowLength = hexSize-1;



		// 4 = Mountain (unwalkable)
		// 3 = Rock Rough
		// 2 = Rock Light
		// 1 = Sand Rough
		// 0 = Sand Light

		// Filling Grid with Mountain Nodes
		for (int i = 0; i < numberOfMountains; i++) {

			//  --------rows, columns
			// HexGrid [5   ,    10] = 2;
			//	HexGrid [Random.Range (0 + maxSizeOfMountains + maxSizeOfRocks, colLength - maxSizeOfRocks) , Random.Range (0 + maxSizeOfMountains + maxSizeOfRocks, rowLength - maxSizeOfRocks)] = 4;

		}  


		// First for loops scan Hex grid for Mountain Nodes
		// Rows iterator
		for (int x = 0; x < rowLength; x++) {
			// Columns iterator
			for (int y = 0; y < colLength; y++) {

				if (HexGrid [x, y] == 4) {
					// When Island node found assign
					// Different size of island each iteration
					int islandSize = Random.Range(minSizeOfMountains , maxSizeOfMountains+1);

					// Second for loops place more Mountain pieces around Mountain Node
					// Rows iterator
					for (int i = x - islandSize ; i <= x ; i++) {
						// Columns iterator
						for (int j = y - islandSize; j <= y; j++) {

							HexGrid [i, j] = 4;

							// a layer of shallow sea tiles set around it
							int rockSize = Random.Range(minSizeOfRocks , maxSizeOfRocks+1);

							// Third for loops Each added mountain tile assigns 
							// a layer of rock tiles set around it
							// Rows iterator
							for (int c = i - rockSize; c <= i + rockSize; c++) {
								// Columns iterator
								for (int k = j - rockSize; k <= j + rockSize; k++) {

									// Assigning rock tiles
									if (HexGrid [c, k] == 4) {
										HexGrid [c, k] = 4;
									} else {
										HexGrid [c, k] = 2;
									}
								}
							}
						}
					}
				}
			} 
		}
	}




	// Fill Hex with map tiles corresponding to integer values
	void PopulateHex() {

		// Geographical Placement of tiles 
		float colPlace = 0;
		float rowPlace = 0;

		// Row and Column Count 
		int rowCount = 0;
		int colCount = 0;

		// hex rows sitting at different positions
		int offset = 0;

		// X value
		int colLength = hexSize-1;
		// Y value
		int rowLength = hexSize-1;

		// Assigns Tiles to Grid Numbers
		// Rows iterator
		for (int i = 0; i <= rowLength; i++) {
			// Columns iterator
			for (int j = 0; j <= colLength; j++) {

				if (HexGrid [rowCount, colCount] == 0) {
					Transform sand_Light = Instantiate (sand_LightPrefab[Random.Range (0, sand_LightPrefab.Length)]);
					sand_Light.transform.Translate (new Vector2 (colPlace, rowPlace));
					sand_Light.transform.parent = GameObject.Find("Terrain_Layer").transform;
				}
				if (HexGrid [rowCount, colCount] == 1) {
					Transform sea_RoughTile = Instantiate (sand_RoughPrefab[Random.Range (0, sand_RoughPrefab.Length)]);
					sea_RoughTile.transform.Translate (new Vector2 (colPlace, rowPlace));
					sea_RoughTile.transform.parent = GameObject.Find("Terrain_Layer").transform;
				}
				if (HexGrid [rowCount, colCount] == 2) {
					Transform sand_Rough = Instantiate (rock_LightPrefab[Random.Range (0, rock_LightPrefab.Length)]);
					sand_Rough.transform.Translate (new Vector2 (colPlace, rowPlace));
					sand_Rough.transform.parent = GameObject.Find("Terrain_Layer").transform;
				}
				if (HexGrid [rowCount, colCount] == 3) {
					Transform rock_Rough = Instantiate (rock_RoughPrefab[Random.Range (0, rock_RoughPrefab.Length)]);
					rock_Rough.transform.Translate (new Vector2 (colPlace, rowPlace));
					rock_Rough.transform.parent = GameObject.Find("Terrain_Layer").transform;
				}
				if (HexGrid [rowCount, colCount] == 4) {
					Transform mountain = Instantiate (mountainPrefab[Random.Range (0, mountainPrefab.Length)]);
					mountain.transform.Translate (new Vector2 (colPlace, rowPlace));
					mountain.transform.parent = GameObject.Find("Terrain_Layer").transform;
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
			colCount = 0;

		}
	}
}

