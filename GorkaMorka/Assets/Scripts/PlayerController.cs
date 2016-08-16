using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameMaster gameMaster;
	Tiles_Layer tiles_Layer;
	Canvas canvas;

	// Game Models //
	public GameObject TrukkPrefab;
	public GameObject Orc01Prefab;
	public GameObject Orc02Prefab;


	// References to the tiles models start on //
	public int startingTile01;
	public int startingTile02;
	public int startingTile03;
	Transform tile01;
	Transform tile02;
	Transform tile03;
	/////////////////////////////////////////////


	// When a PLayer is selected //
	public bool modelSelected;
	public GameObject ModelSelectedObject;
	public GameObject ModelsTileObject;
	public int modelsTileNum;
	/// ////////////////////////////


	// When a PLayer is selected and a another tile is selected //
	public bool movementSelected;
	public GameObject tileToMoveToObject;
	public int TileToMoveToNum;
	/// ////////////////////////////

	// 1 = walking
	// 2 = run
	// 3 = overwatch
	public int moveType;
	public int walkingArea;
	public int runningArea;
	private int area = 0;


	public Transform[] MovementSpreadArray = new Transform[2];
	public int arrayCount;
	Transform tile;

	// Use this for initialization
	void Start () {

		gameMaster = GameObject.Find ("GameMaster").transform.GetComponent<GameMaster> ();
		tiles_Layer = GameObject.Find ("Tiles_Layer").transform.GetComponent<Tiles_Layer> ();
		canvas = GameObject.Find ("Canvas_Panels").transform.GetComponent<Canvas> ();

		// When a PLayer is selected //
		ModelSelectedObject = null;
		modelSelected = false;
		modelsTileNum = 0;
		/////////////////////////////
		// When a PLayer is selected and a another tile is selected //
		movementSelected = false;
		tileToMoveToObject = null;
		TileToMoveToNum = 0;
		////////////////////////////
		runningArea = walkingArea*2;

		StartCoroutine (Wait ());
	}


	IEnumerator Wait () {
		// Must wait couple micro seconds to place models so all
		// Tiles can load.
		yield return new WaitForSeconds (0.2f);
		//// Tiles that the models are placed on //
		tile01 = tiles_Layer.gameObject.transform.GetChild (startingTile01-1);
		tile02 = tiles_Layer.gameObject.transform.GetChild (startingTile02-1);
		tile03 = tiles_Layer.gameObject.transform.GetChild (startingTile03-1);
		/////
		PlaceModels ();
	}



	void PlaceModels() {
		Debug.Log (tile01.GetComponent<Tiles>().tileCounter + " " + tile02.GetComponent<Tiles>().tileCounter + " " + tile03.GetComponent<Tiles>().tileCounter);

		GameObject Trukk = Instantiate (TrukkPrefab);
		Trukk.transform.SetParent(this.transform);
		Trukk.transform.position = tile01.transform.position;
		Trukk.GetComponentInChildren<Model> ().currentTileNum = (tile01.GetComponent<Tiles>().tileCounter);
		Trukk.GetComponentInChildren<Model> ().currentTileTrans = tile01.transform;
		Trukk.GetComponentInChildren<Model> ().startingTile = tile01.transform;

		GameObject Orc01 = Instantiate (Orc01Prefab);
		Orc01.transform.SetParent(this.transform);
		Orc01.transform.position = tile02.transform.position;
		Orc01.GetComponentInChildren<Model> ().currentTileNum = (tile02.GetComponent<Tiles>().tileCounter);
		Orc01.GetComponentInChildren<Model> ().currentTileTrans = tile02.transform;
		Orc01.GetComponentInChildren<Model> ().startingTile = tile02.transform;

		GameObject Orc02 = Instantiate (Orc02Prefab);
		Orc02.transform.SetParent(this.transform);
		Orc02.transform.position = tile03.transform.position;
		Orc02.GetComponentInChildren<Model> ().currentTileNum = (tile03.GetComponent<Tiles>().tileCounter);
		Orc02.GetComponentInChildren<Model> ().currentTileTrans = tile03.transform;
		Orc02.GetComponentInChildren<Model> ().startingTile = tile03.transform;
	}
		

	public void PlayerSelected(bool selected, GameObject selectedModel, GameObject currentTile, int currentTileNum) {

		// When a PLayer is selected //
		modelSelected = selected;
		ModelSelectedObject = selectedModel;
		ModelsTileObject = currentTile;
		modelsTileNum = currentTileNum;

		// Turn tile green //
		ModelsTileObject.transform.GetComponent<Tiles> ().TileSelect ();
		// Make canvas vehicle panel appear //
		canvas.transform.GetChild (1).gameObject.SetActive(true);
		
	}


	public void PlayerDeSelected() {

		ModelsTileObject.transform.GetComponent<Tiles> ().TileDeSelect ();
		// Make canvas vehicle panel dissappear //
		canvas.transform.GetChild (1).gameObject.SetActive(false);

		// When a PLayer is selected //
		modelSelected = false;
		ModelSelectedObject = null;
		ModelsTileObject = null;
		modelsTileNum = 0;

		movementSelected = false;
		tileToMoveToObject = null;
		TileToMoveToNum = 0;

	}


	public void PlayerMove(bool selected, GameObject tileToMoveTo, int tileNumToMoveTo) {

		movementSelected = selected;
		tileToMoveToObject = tileToMoveTo;
		TileToMoveToNum = tileNumToMoveTo;

		// Turn current tile transparent //
		ModelsTileObject.GetComponent<Tiles> ().TileDeSelect ();
		// Turn new tile to move to green //
		tileToMoveToObject.GetComponent<Tiles> ().TileSelect ();

		// Make old tile reference point to new tile //
		ModelsTileObject = tileToMoveToObject;

		// Move model to new tile //
		ModelSelectedObject.GetComponent<Model> ().ModelMove (tileToMoveToObject);
		// Set models new surrent tile number and transform to new tile //
		ModelSelectedObject.GetComponent<Model> ().currentTileNum = tileNumToMoveTo;
		ModelSelectedObject.GetComponent<Model> ().currentTileTrans = tileToMoveTo.transform;


	}



	public void PlayerMovementAreaDisplay(Transform currentTileTrans, int currentTileNum) {

		arrayCount = 0;

		if (moveType == 1) {
			area = walkingArea;
		} else if (moveType == 2) {
			area = runningArea;
		} else if (moveType == 3) {
			area = 1;
		} else {
			area = 0;
		}

		////////////////////////////////////////
		//Left spread//
		if (currentTileNum - area > 0) {
			for (int i = (currentTileNum - 1); i >= (currentTileNum - area); i--) {

				tile = tiles_Layer.gameObject.transform.GetChild (i - 1);
		
				MovementSpreadArray [arrayCount] = tile;
				arrayCount++;
			}
		}
		//Right Spread//
		if (currentTileNum + area < gameMaster.tileCount) {
			for (int i = (currentTileNum); i < (currentTileNum + area); i++) {

				tile = tiles_Layer.gameObject.transform.GetChild (i);
		
				MovementSpreadArray [arrayCount] = tile;
				arrayCount++;
			}
		}
		///////////////////////////////////////



		///////////////////////////////////////
		// This check is to align tiles properly //
		int test = (currentTileNum/10);
		if (test % 2 != 0) {
			
			//Infront spread//
			if (currentTileNum + 10 <= gameMaster.tileCount) {
				for (int i = ((currentTileNum + 10) - area); i < ((currentTileNum + 10) + area); i++) {

					tile = tiles_Layer.gameObject.transform.GetChild (i);
		
					MovementSpreadArray [arrayCount] = tile;
					arrayCount++;
				}
			}
			//Back spread//
			if (currentTileNum - 10 > 0) {
				for (int i = ((currentTileNum - 10) - area); i < ((currentTileNum - 10) + area); i++) {

					tile = tiles_Layer.gameObject.transform.GetChild (i);
	
					MovementSpreadArray [arrayCount] = tile;
					arrayCount++;
				}
			}

		} else {
			
			//Infront spread//
			if (currentTileNum + 10 <= gameMaster.tileCount) {
				for (int i = ((currentTileNum + 10) - area); i < ((currentTileNum + 10) + area); i++) {

					tile = tiles_Layer.gameObject.transform.GetChild (i - 1);

					MovementSpreadArray [arrayCount] = tile;
					arrayCount++;
				}
			}
			//Back spread//
			if (currentTileNum - 10 > 0) {
				for (int i = ((currentTileNum - 10) - area); i < ((currentTileNum - 10) + area); i++) {

					tile = tiles_Layer.gameObject.transform.GetChild (i - 1);

					MovementSpreadArray [arrayCount] = tile;
					arrayCount++;
				}
			}
		}
		///////////////////////////////////////



		///////////////////////////////////////
		//Top spread//
		if (currentTileNum + 20 <= gameMaster.tileCount) {
			for (int i = ((currentTileNum + 20) - (area)); i < ((currentTileNum + 20) + (area - 1)); i++) {

				tile = tiles_Layer.gameObject.transform.GetChild (i);
	
				MovementSpreadArray [arrayCount] = tile;
				arrayCount++;
			}
		}
		//Bottom spread//
		if (currentTileNum - 20 > 0) {
			for (int i = ((currentTileNum - 20) - (area)); i < ((currentTileNum - 20) + (area - 1)); i++) {

				tile = tiles_Layer.gameObject.transform.GetChild (i);
	
				MovementSpreadArray [arrayCount] = tile;
				arrayCount++;
			}
		}
		/////////////////////////////////////////

	
		//////////////////////////////////////


		// Reveal movement spread //
		for (int i = 0; i < arrayCount; i++) {
			MovementSpreadArray[i].GetComponent<Tiles> ().TileInMovementArea ();
		}
		////////////////////////////

		// Have to put middle tile in array //
		MovementSpreadArray [arrayCount] = ModelsTileObject.transform;
		arrayCount++;
		//  and change to Green? //
		tiles_Layer.gameObject.transform.GetChild (currentTileNum-1).transform.GetComponent<Tiles> ().TileSelect();
	}


	public void PlayerMovementAreaClear() {

		// Clear movement spread //
		for (int i = 0; i < arrayCount; i++) {
			MovementSpreadArray[i].GetComponent<Tiles> ().TileOutMovementArea ();
		}

		// Set type back to 0 //
		moveType = 0;

		// zero out tile array //
		((IList)MovementSpreadArray).Clear ();

	}
}
